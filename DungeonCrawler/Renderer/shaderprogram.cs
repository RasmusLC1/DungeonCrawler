using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace ZombieGame.Renderer{
    public readonly struct ShaderUniform {
        public readonly string Name;
        public readonly int Location;
        public readonly ActiveUniformType Type;    


        public ShaderUniform (string name, int location, ActiveUniformType type){
            this.Name = name;
            this.Location = location;
            this.Type = type;
        }
    }

    public readonly struct ShaderAttribute {
        public readonly string Name;
        public readonly int Location;
        public readonly ActiveAttribType Type;    


        public ShaderAttribute (string name, int location, ActiveAttribType type){
            this.Name = name;
            this.Location = location;
            this.Type = type;
        }
    }

    sealed public class ShaderProgram : IDisposable{
        private bool disposed;
        public readonly int shaderProgramHandle;
        public readonly int vertexShaderHandle;
        public readonly int pixelShaderHandle;

        private readonly ShaderUniform[] uniforms;
        private readonly ShaderAttribute[] attributes;


        public ShaderProgram(string vertexShaderCode, string pixelShaderCode){
            this.disposed = false;

            //Setting up the vertex
            if (!ShaderProgram.CompileVertexShader(vertexShaderCode, out this.vertexShaderHandle, out string vertexShaderCompileError)){
                throw new ArgumentException(vertexShaderCompileError);
            }

            //Setting up the colour
            if (!ShaderProgram.CompilePixelShader(pixelShaderCode, out this.pixelShaderHandle, out string pixelShaderCompileError)){
                throw new ArgumentException(pixelShaderCompileError);
            }

            //Linking the vertex and pixel to the program
            this.shaderProgramHandle = ShaderProgram.CreateLinkProgram(this.vertexShaderHandle, this.pixelShaderHandle);

            this.uniforms = ShaderProgram.CreateUniformList(this.shaderProgramHandle);    
            this.attributes = ShaderProgram.CreateAttributeList(this.shaderProgramHandle);    

        }


        ~ShaderProgram() {
            this.Dispose();
        }

        public void Dispose(){
            if (this.disposed){
                return;
            }

            //Delete them to free up memory
            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(pixelShaderHandle);

            this.disposed = true;
            //If we called this we don't need to call the finaliser anymore
            GC.SuppressFinalize(this); 
            //Make sure we use no program and then delete the program handler
            GL.UseProgram(0);
            GL.DeleteProgram(this.shaderProgramHandle);
        }

        private static bool CompileVertexShader(string vertexShaderCode, out int vertexShaderHandle, out string errorMessage){
            
            errorMessage = string.Empty;
            //Error checking, gives you the EnableVertexAttribArray with a linenumber so 0.13 would be vertexShader line 13
            vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader); //Create handler for the shader
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode); // Enter the sourcecode (vertexShaderCode) and the handler
            GL.CompileShader(vertexShaderHandle); //Compile the code

            string vertxShaderErrormsg = GL.GetShaderInfoLog(vertexShaderHandle);

            if (vertxShaderErrormsg != string.Empty){
                errorMessage = vertxShaderErrormsg;
                return false;
            }
            return true;
        }

        private static bool CompilePixelShader(string pixelShaderCode, out int pixelShaderHandle, out string errorMessage){
            
            errorMessage = string.Empty;
            //Error checking, gives you the EnableVertexAttribArray with a linenumber so 0.13 would be vertexShader line 13
            pixelShaderHandle = GL.CreateShader(ShaderType.FragmentShader); //Create handler for the shader
            GL.ShaderSource(pixelShaderHandle, pixelShaderCode); // Enter the sourcecode (pixelShaderCode) and the handler
            GL.CompileShader(pixelShaderHandle); //Compile the code


            //Error checking
            string pixelShaderErrormsg = GL.GetShaderInfoLog(pixelShaderHandle);
            if (pixelShaderErrormsg != string.Empty){
                errorMessage = pixelShaderErrormsg;
                return false;
            }
            return true;
        }
        public static int CreateLinkProgram(int vertexShaderHandle, int pixelShaderHandle){
            int shaderProgramHandle = GL.CreateProgram();

            //Attach the two shaders to the main program
            GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgramHandle, pixelShaderHandle);
            GL.LinkProgram(shaderProgramHandle);

            //Detach them so we can delete them as they are no longer used
            GL.DetachShader(shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(shaderProgramHandle, pixelShaderHandle);
            return shaderProgramHandle;
        }


        public static ShaderUniform[] CreateUniformList(int shaderProgramHandle){
            GL.GetProgram(shaderProgramHandle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            ShaderUniform[] uniforms = new ShaderUniform[uniformCount];

            for (int i = 0; i < uniformCount; i++) {
                GL.GetActiveUniform(shaderProgramHandle, i, 256, out _, out _, out ActiveUniformType type, out string name);
                int location = GL.GetUniformLocation(shaderProgramHandle, name);
                uniforms[i] = new ShaderUniform(name, location, type);
            }
            return uniforms;
        }

        public static ShaderAttribute[] CreateAttributeList(int shaderProgramHandle){
            GL.GetProgram(shaderProgramHandle, GetProgramParameterName.ActiveUniforms, out int attributeCount);
            ShaderAttribute[] attributes = new ShaderAttribute[attributeCount];

            for (int i = 0; i < attributeCount; i++) {
                GL.GetActiveAttrib(shaderProgramHandle, i, 256, out _, out _, out ActiveAttribType type, out string name);
                int location = GL.GetAttribLocation(shaderProgramHandle, name);
                attributes[i] = new ShaderAttribute(name, location, type);
            }
            return attributes;
        }

        public ShaderUniform[] GetUniformList(){
            ShaderUniform[] result = new ShaderUniform[this.uniforms.Length];
            Array.Copy(this.uniforms, result, this.uniforms.Length);
            return result;
        }
        public ShaderAttribute[] GetAttributeList(){
            ShaderAttribute[] result = new ShaderAttribute[this.uniforms.Length];
            Array.Copy(this.uniforms, result, this.uniforms.Length);
            return result;
        }

        public void SetUniform(string name, float v1)
        {
            if(!this.GetShaderUniform(name, out ShaderUniform uniform))
            {
                throw new ArgumentException("Name was not found.");
            }

            if(uniform.Type != ActiveUniformType.Float)
            {
                throw new ArgumentException("Uniform type is not float.");
            }

            GL.UseProgram(this.shaderProgramHandle);
            GL.Uniform1(uniform.Location, v1);
            GL.UseProgram(0);
        }

        public void SetUniform(string name, float v1, float v2)
        {
            if (!this.GetShaderUniform(name, out ShaderUniform uniform))
            {
                throw new ArgumentException("Name was not found.");
            }

            if (uniform.Type != ActiveUniformType.FloatVec2)
            {
                throw new ArgumentException("Uniform type is not FloatVec2.");
            }

            GL.UseProgram(this.shaderProgramHandle);
            GL.Uniform2(uniform.Location, v1, v2);
            GL.UseProgram(0);
        }


        private bool GetShaderUniform(string name, out ShaderUniform uniform)
        {
            uniform = new ShaderUniform();

            for(int i = 0; i < this.uniforms.Length; i++)
            {
                uniform = this.uniforms[i];

                if(name == uniform.Name)
                {
                    return true;
                }
            }

            return false;
        }

        
    }
}
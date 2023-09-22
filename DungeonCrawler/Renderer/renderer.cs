using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Reflection.Metadata;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZombieGame.Renderer{
public class GraphicsRenderer{
    private VertexBuffer vertexBuffer;
    private IndexBuffer indexBuffer;
    private VertexArray vertexArray;
    private ShaderProgram shaderProgram;
    private string vertexShaderCode;
    private string pixelShaderCode;
    private int vertexCount;
    private int indexCount;

    public void FrameRenderer(FrameEventArgs args, GameWindow window){
        // GL.Clear(ClearBufferMask.ColorBufferBit); // Clear the screen

        GL.UseProgram(this.shaderProgram.shaderProgramHandle); // Use our program
        
        GL.BindVertexArray(this.vertexArray.vertexArrayHandle);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBuffer.indexBufferHandle);

        GL.DrawElements(PrimitiveType.Triangles, indexCount, DrawElementsType.UnsignedInt, 0); // First argument is type of shape, second is index of array should always by times 3 for triangles, third is amount of entries

        window.Context.SwapBuffers();
    }
    public void OnUnload(){
        // Dispose of the buffers, use the ? to check for null
        this.vertexArray?.Dispose();
        this.indexBuffer?.Dispose();
        this.vertexBuffer?.Dispose();
    }
    public void Setup(GameWindow window){
        window.IsVisible = true;

        GL.ClearColor(Color4.Aqua); //Background colour

        
    }
    public void Draw(GameWindow window){
            int winWidth = window.ClientSize.X;
            int winHeight = window.ClientSize.Y;
            int boxCount = 10; //Amount of boxes to be created

            Random rand = new Random();

            this.vertexCount = 0;
            VertexPositionColour[] vertices = new VertexPositionColour[boxCount*4]; //Times 4 since there are 4 colour values

            //Create 4 random verticies for each boxCount
            for (int i = 0; i < boxCount; i++) {

                //random Dimensions
                int w = rand.Next(32, 128);
                int h = rand.Next(32, 128);
                int x = rand.Next(0, winWidth - w);
                int y = rand.Next(0, winHeight - h);

                //random Colour
                float r = (float)rand.NextDouble();
                float g = (float)rand.NextDouble();
                float b = (float)rand.NextDouble();

                //vertices creation
                vertices[this.vertexCount++] = new VertexPositionColour(new Vector2(x, y + h), new Color4(r, g, b, 1f)); //Top-Left vertex position(2 floats) Colour(4 floats)
                vertices[this.vertexCount++] = new VertexPositionColour(new Vector2(x+w, y+h), new Color4(r, g, b, 1f)); //Top-Right
                vertices[this.vertexCount++] = new VertexPositionColour(new Vector2(x+w, y), new Color4(r, g, b, 1f)); //Bottom-Right
                vertices[this.vertexCount++] = new VertexPositionColour(new Vector2(x, y), new Color4(r, g, b, 1f)); //Bottom-Left
            }
        
            VerticesSetup(vertices, boxCount);
            ShaderSetup();
        }


        private void VerticesSetup(VertexPositionColour[]vertices, int boxCount){
            //Tells the order which the indicies should be read
            int[] indicies = new int[boxCount * 6]; //Times 6 since there are 6 vertices
            
            //Reset the counts
            this.indexCount = 0;
            this.vertexCount = 0;


            //Old indicies logic for reference
            // int[] indicies = new int[]{
            //     0, 1, 2, 0, 2, 3
            // };


            //setup the indices order for each box
            for (int i = 0; i < boxCount; i++) {
                indicies[this.indexCount++] = 0 + this.vertexCount;
                indicies[this.indexCount++] = 1 + this.vertexCount;
                indicies[this.indexCount++] = 2 + this.vertexCount;
                indicies[this.indexCount++] = 0 + this.vertexCount;
                indicies[this.indexCount++] = 2 + this.vertexCount;
                indicies[this.indexCount++] = 3 + this.vertexCount;
                this.vertexCount += 4; //Increment by one for each unique vertice
            }


            //VertexBuffer setup
            this.vertexBuffer = new VertexBuffer(VertexPositionColour.VertexInfo, vertices.Length, true);
            this.vertexBuffer.SetData(vertices,vertices.Length);

            //IndexBuffer setup
            this.indexBuffer = new IndexBuffer(indicies.Length, true);
            this.indexBuffer.SetData(indicies, indicies.Length);

            this.vertexArray = new VertexArray(this.vertexBuffer);

            GL.BindVertexArray(0);
        }
        private void ShaderSetup(){
            //Vertex shader for the position of the vectors
            vertexShaderCode = @"
                                    #version 330 core

                                    uniform vec2 ViewportSize;

                                    layout (location = 0) in vec2 aPosition;
                                    layout (location = 1) in vec4 aColour;

                                    out vec4 vColour;

                                    void main(){
                                        float nx = aPosition.x / ViewportSize.x * 2.0 - 1.0;
                                        float ny = aPosition.y / ViewportSize.y * 2.0 - 1.0;
                                        gl_Position = vec4(nx, ny, 0.0, 1.0);

                                        vColour = aColour;
                                    }
                                    ";
            
            // shader for the colour of the vectors
            pixelShaderCode = @"
                            #version 330 core

                            in vec4 vColour;

                            out vec4 pixelColour;

                            void main() {
                                pixelColour = vColour;
                            }
                            ";
            
            this.shaderProgram = new ShaderProgram(vertexShaderCode, pixelShaderCode);

            //Convert the coordinates to standard 0 to width/height instead of -1 to 1
            int [] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);
            GL.UseProgram(this.shaderProgram.shaderProgramHandle); //enable our program to activate changes
            int viewportSizeUniformLocation = GL.GetUniformLocation(this.shaderProgram.shaderProgramHandle, "ViewportSize"); //Give the uniform location
            GL.Uniform2(viewportSizeUniformLocation, (float)viewport[2], (float)viewport[3]); //UniformX the number just means how many inputs it gets,
                                                                                            // here we have 2 since ViewportSize is a vec2
            GL.UseProgram(0); //Disable the program again
        }
    }
}
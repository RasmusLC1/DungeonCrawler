using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace ZombieGame.Renderer{

    public sealed class VertexBuffer : IDisposable{
        public readonly int vertexBufferHandle;
        public readonly VertexInfo VertexInfo;
        public readonly int VertexCount;
        public readonly bool IsStatic;
        private bool disposed;
        public static readonly int minVertexCount = 1;
        public static readonly int maxVertexCount = 100_000;
        public VertexBuffer(VertexInfo vertexInfo, int vertexCount, bool isStatic = true){
            this.disposed = false;

            if(vertexCount < VertexBuffer.minVertexCount ||
                vertexCount > VertexBuffer.maxVertexCount)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexCount));
            }

            this.VertexInfo = vertexInfo;
            this.VertexCount = vertexCount;
            this.IsStatic = isStatic;

            BufferUsageHint hint = BufferUsageHint.StaticDraw;
            if(!this.IsStatic)
            {
                hint = BufferUsageHint.StreamDraw;
            }

            int vertexSizeInBytes = VertexPositionColour.VertexInfo.SizeInBytes;

            this.vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, this.VertexCount * this.VertexInfo.SizeInBytes, IntPtr.Zero, hint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        //~ means that it's a finaliser
        ~VertexBuffer(){
            this.Dispose();
        }
        /// <summary>
        /// Allows us to verify and set our data with a type T which will always be a struct
        /// </summary>
        public void SetData<T>(T[] data, int dataCount) where T : struct{
            //Error handling
            if (typeof(T) != this.VertexInfo.Type){
                throw new ArgumentException("Type T does not match Vertex type");
            }
            if (data is null){
                throw new ArgumentNullException(nameof(data));
            }
            if (data.Length <= 0){
                throw new ArgumentOutOfRangeException(nameof(data));
            }
            if (dataCount <= 0 || dataCount > this.VertexCount || dataCount > data.Length){
                throw new ArgumentOutOfRangeException(nameof(dataCount));
            }

            //Allows us to set the data to a vertex array
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, dataCount * this.VertexInfo.SizeInBytes, data);
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
        }
        public void Dispose(){
            if (this.disposed){
                return;
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(this.vertexBufferHandle);
            this.disposed = true;
            //If we called this we don't need to call the finaliser anymore
            GC.SuppressFinalize(this); 
        }
        
    }
}
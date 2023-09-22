using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace ZombieGame.Renderer{

    public sealed class IndexBuffer : IDisposable{
        private bool disposed;
        public readonly int indexBufferHandle;
        public readonly int IndexCount;
        public readonly bool IsStatic;
        public readonly int minIndexCount = 1;
        public readonly int maxIndexCount = 250_000;
        public IndexBuffer(int indexCount, bool isStatic = true){
            IndexCount = indexCount;
            IsStatic = isStatic;

            BufferUsageHint hint = BufferUsageHint.StaticDraw;
            if(!this.IsStatic)
            {
                hint = BufferUsageHint.StreamDraw;
            }


            this.indexBufferHandle = GL.GenBuffer(); //generate indexBuffer
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBufferHandle); //Bind indexBuffer
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.IndexCount * sizeof(int), IntPtr.Zero, hint); //send data
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); //Ubind the indexBuffer
        }
        ~IndexBuffer() {
            this.Dispose();
        }

        public void Dispose(){
            if (this.disposed){
                return;
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(this.indexBufferHandle);
            this.disposed = true;
            //If we called this we don't need to call the finaliser anymore
            GC.SuppressFinalize(this); 
        }
        public void SetData(int[] data, int dataCount){
            if (data is null){
                throw new ArgumentNullException(nameof(data));
            }
            if (data.Length <= 0){
                throw new ArgumentOutOfRangeException(nameof(data));
            }
            if (dataCount <= 0 || dataCount > this.IndexCount || dataCount > data.Length){
                throw new ArgumentOutOfRangeException(nameof(dataCount));
            }

            //Allows us to set the data to a vertex array
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBufferHandle);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, dataCount * sizeof(int), data);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer,0);
        }
    }
}
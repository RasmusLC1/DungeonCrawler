using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace ZombieGame.Renderer {

    public sealed class VertexArray : IDisposable {
        private bool disposed;

        public readonly int vertexArrayHandle;
        public readonly VertexBuffer VertexBuffer;

        public VertexArray(VertexBuffer vertexBuffer) {
            this.disposed = false;

            if(vertexBuffer is null) {
                throw new ArgumentNullException(nameof(vertexBuffer));
            }

            this.VertexBuffer = vertexBuffer;

            int vertexSizeInBytes = this.VertexBuffer.VertexInfo.SizeInBytes;
            VertexAttribute[] attributes = this.VertexBuffer.VertexInfo.VertexAttributes;


            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VertexBuffer.vertexBufferHandle);

            for(int i = 0; i < attributes.Length; i++) {
                VertexAttribute attribute = attributes[i];
                GL.VertexAttribPointer(attribute.Index, attribute.ComponentCount, VertexAttribPointerType.Float, false, vertexSizeInBytes, attribute.Offset);
                GL.EnableVertexAttribArray(attribute.Index);
            }

            GL.BindVertexArray(0);
        }

        ~VertexArray() {
            this.Dispose();
        }

        public void Dispose() {
            if(this.disposed) {
                return;
            }

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(this.vertexArrayHandle);

            this.disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
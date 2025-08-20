using System;
using System.IO;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace MorseCode.UWP.Classes
{
    public class MemoryRandomAccessStream : IRandomAccessStream
    {
        private Stream m_InternalStream;

        public MemoryRandomAccessStream(Stream stream)
        {
            m_InternalStream = stream;
        }

        public MemoryRandomAccessStream(byte[] bytes)
        {
            m_InternalStream = new MemoryStream(bytes);
        }

        public IInputStream GetInputStreamAt(ulong position)
        {
            m_InternalStream.Seek((long)position, SeekOrigin.Begin);

            return m_InternalStream.AsInputStream();
        }

        public IOutputStream GetOutputStreamAt(ulong position)
        {
            m_InternalStream.Seek((long)position, SeekOrigin.Begin);

            return m_InternalStream.AsOutputStream();
        }

        public ulong Size
        {
            get { return (ulong)m_InternalStream.Length; }
            set { m_InternalStream.SetLength((long)value); }
        }

        public bool CanRead
        {
            get { return true; }
        }

        public bool CanWrite
        {
            get { return true; }
        }

        public IRandomAccessStream CloneStream()
        {
            if (m_InternalStream is MemoryStream memoryStream)
            {
                var buffer = memoryStream.ToArray();
                var clone = new MemoryRandomAccessStream(buffer);
                clone.Seek((ulong)memoryStream.Position);
                return clone;
            }
            throw new NotSupportedException("CloneStream is only supported for MemoryStream.");
        }

        public ulong Position
        {
            get { return (ulong)m_InternalStream.Position; }
        }

        public void Seek(ulong position)
        {
            m_InternalStream.Seek((long)position, 0);
        }

        public void Dispose()
        {
            m_InternalStream.Dispose();
        }

        public IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            var inputStream = GetInputStreamAt(0);
            return inputStream.ReadAsync(buffer, count, options);
        }

        public IAsyncOperation<bool> FlushAsync()
        {
            var outputStream = GetOutputStreamAt(0);
            return outputStream.FlushAsync();
        }

        public IAsyncOperationWithProgress<uint, uint> WriteAsync(IBuffer buffer)
        {
            var outputStream = GetOutputStreamAt(0);
            return outputStream.WriteAsync(buffer);
        }
    }
}

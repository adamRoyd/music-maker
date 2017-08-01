using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace MusicMaker
{
    class ClippingSampleProvider : ISampleProvider
    {
        private ISampleProvider source;

        public ClippingSampleProvider(ISampleProvider source)
        {
            this.source = source;
        }
        WaveFormat ISampleProvider.WaveFormat
        {
            get { return source.WaveFormat; }
        }

        int ISampleProvider.Read(float[] buffer, int offset, int count)
        {
            int sampleRead = source.Read(buffer, offset, count);
            for (int n = 0; n < sampleRead; n++)
            {
                if (buffer[offset + n] > 1.0f)
                    buffer[offset + n] = 1.0f;
                else if (buffer[offset + n] < -1.0f)
                    buffer[offset + n] = -1.0f;
            }
            return sampleRead;
        }
    }
}

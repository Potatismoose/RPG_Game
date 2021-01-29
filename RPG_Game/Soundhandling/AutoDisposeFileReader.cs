using NAudio.Wave;

namespace RPG_Game
{
    //This is code that I found online for the soundhandling. Not written by me.
    // https://markheath.net/post/fire-and-forget-audio-playback-with

    /*
     This is needed to dispose the sound after it´s used. 
    Otherwise it will flood the memory, creating a "memory leak" or more correctly a memory flood.
    Dispose the sound by using the created sound object .dispose()
     */
    class AutoDisposeFileReader : ISampleProvider
    {
        private readonly AudioFileReader reader;
        private bool isDisposed;
        public AutoDisposeFileReader(AudioFileReader reader)
        {
            this.reader = reader;
            this.WaveFormat = reader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (isDisposed)
                return 0;
            int read = reader.Read(buffer, offset, count);
            if (read == 0)
            {
                reader.Dispose();
                isDisposed = true;
            }
            return read;
        }

        public WaveFormat WaveFormat { get; private set; }
    }
}

using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace RPG_Game
{

    //This is code that I found online for the soundhandling. Not written by me.
    // https://markheath.net/post/fire-and-forget-audio-playback-with

    //Essetially what it does is creating a "player" for the sound, and it can handle all the output and input.
    /*
     Volume, start stop, sample rates, and this is the class that handles the mixer. 
    That´s needed for playing two sounds at the same time.

    I´ve added some functionallity myself by adding raise and lower volume, resume and some other things.
     */


    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice.Init(mixer);
            outputDevice.Volume = 0.15f;
            outputDevice.Play();

        }

        public void PlaySound(string fileName)
        {
            var input = new AudioFileReader(fileName);
            AddMixerInput(new AutoDisposeFileReader(input));

        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented");
        }

        public void PlaySound(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        private void AddMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public void StopSound()
        {
            outputDevice.Stop();

        }
        public void PauseSound()
        {
            outputDevice.Pause();

        }

        public void RaiseVol()
        {
            outputDevice.Volume = 1f;
        }

        public void LowerVol()
        {
            outputDevice.Volume = 0.15f;
        }


        public void ResumeSound()
        {
            outputDevice.Play();

        }


        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }
}

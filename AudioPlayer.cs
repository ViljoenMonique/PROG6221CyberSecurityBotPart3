using System;
using System.Media;

namespace CyberSecurityBot
{
    public class AudioPlayer
    {
        public void PlayGreeting()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(@"Assets\greeting.wav"))
                {
                    player.PlaySync();
                }
            }
            catch { }
        }
    }
}
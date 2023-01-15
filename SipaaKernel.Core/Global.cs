using Cosmos.System.Network;
using Cosmos.System.FileSystem;
using Cosmos.System.Audio;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System.Audio.IO;

namespace SipaaKernel.Core
{
    public class Global
    {
        static AudioManager audioManager;
        static AudioMixer audioMixer;
        static AC97 ac97;
        /// <summary>
        /// Plays a wave file. 
        /// </summary>
        public static void PlayAudio(AudioStream Stream)
        {
            OnUpdate();
            audioMixer.Streams.Add(Stream);
        }

        /// <summary>
        /// Update the audio manager.
        /// </summary>
        public static void OnUpdate()
        {
            if (audioManager.Stream.Depleted)
            {
                if (audioManager.Enabled)
                {
                    audioManager.Disable();
                }
            }
            else
            {
                if (!audioManager.Enabled)
                {
                    audioManager.Enable();
                }
            }
        }

        /// <summary>
        /// (PRIVATE METHOD) Verify if verbose mode is enabled. If yes, the method will display the exception and hang.
        /// </summary>
        /// <param name="ex">The exception to display</param>
        /// <param name="component">The name of the component</param>
        /// <param name="verbose">Verbose mode</param>
        static void HangOnExceptionIfVerbose(Exception ex, string component, bool verbose)
        {
            if (verbose)
            {
                Console.Clear();
                Console.WriteLine($"Exception occured during the boot of SipaaKernel. ({component} component)");
                Console.WriteLine(ex.Message);

                Console.WriteLine("Press any key to continue boot...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Default boot routines of SipaaKernel
        /// </summary>
        /// <param name="verbose"></param>
        public static void Boot(bool verbose = true, bool silentMode = true)
        {
            #region File system initialization
            if (verbose) Console.WriteLine("Initializing file system...");
            try
            {
                var fs = new CosmosVFS();
                
                VFSManager.RegisterVFS(fs, false);
            }catch (Exception ex)
            {
                if (!silentMode)
                {
                    HangOnExceptionIfVerbose(ex, "File system", verbose);
                }
            }
            #endregion
            #region Network initialization
            if (verbose) Console.WriteLine("Initializing network...");
            try
            {
                _ = new DHCPClient().SendDiscoverPacket();
            }catch (Exception ex)
            {
                if (!silentMode)
                {
                    HangOnExceptionIfVerbose(ex, "Network", verbose);
                }
            }
            #endregion
            #region Audio initialization
            if (verbose) Console.WriteLine("Initializing audio (if you are on VMware, it will make an exception)...");
            try
            {
                audioMixer = new AudioMixer();
                ac97 = AC97.Initialize(bufferSize: 4096);

                var audioManager = new AudioManager()
                {
                    Stream = audioMixer,
                    Output = ac97
                };
            }
            catch (Exception ex)
            {
                if (!silentMode)
                {
                    HangOnExceptionIfVerbose(ex, "Audio", verbose);
                }
            }
            #endregion
            #region Final operations
            if (verbose && !silentMode)
            {
                Console.WriteLine("SipaaKernel booted sucessfully! Press any key to continue");
                Console.ReadKey();
            }
            #endregion
        }
    }
}
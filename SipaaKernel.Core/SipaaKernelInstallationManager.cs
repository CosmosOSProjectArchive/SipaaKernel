using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Core
{
    /// <summary>
    /// Manages your SipaaKernel installation
    /// </summary>
    public class SipaaKernelInstallationManager
    {
        /// <summary>
        /// Check if the system directory exists
        /// </summary>
        public static bool IsInstalled { get => Directory.Exists(SystemDirectory); }

        public const string SystemDirectory = @"0:\SipaaKernel";

        /// <summary>
        /// Install SipaaKernel to the 0:\ partition
        /// </summary>
        public static void Install()
        {
           
            Directory.CreateDirectory(SystemDirectory);
            File.WriteAllText(SystemDirectory + @"\OSVersion.data", "No build :<");
        }

        /// <summary>
        /// Verify the file integrity
        /// 
        /// Return codes :
        /// 0 : Verification passed sucessfully
        /// 1 : SipaaKernel is not installed
        /// 2 : Some files needed is broke.
        /// </summary>
        /// <returns>If the system should crash or launch setup or do nothing.</returns>
        public static int VerifyFileIntegrity()
        {
            int FileExistingCount = 0;
            int FileWithGoodDataCount = 0;

            if (IsInstalled)
            {
                if (File.Exists(SystemDirectory + @"\OSVersion.data"))
                {
                    FileExistingCount += 1;
                }
                
                if (File.ReadAllText(SystemDirectory + @"\OSVersion.data") == "No build :<")
                {
                    FileWithGoodDataCount += 1;
                }

                if (FileExistingCount == 1 && FileWithGoodDataCount == 1)
                {
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}

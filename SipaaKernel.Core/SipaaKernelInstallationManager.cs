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
    internal class SipaaKernelInstallationManager
    {
        /// <summary>
        /// Check if the system directory exists
        /// </summary>
        public static bool IsInstalled { get => Directory.Exists(SystemDirectory); }

        public const string SystemDirectory = @"0:\SipaaKernel";

        /// <summary>
        /// Verify the file integrity
        /// 
        /// Return codes :
        /// 0 : Verification passed sucessfully
        /// 1 : SipaaKernel is not installed
        /// 2 : Some files needed is broke.
        /// </summary>
        /// <returns>If the system should crash or launch setup or do nothing.</returns>
        public int VerifyFileIntegrity()
        {
            int FileExistingCount = 0;
            int FileWithGoodDataCount = 0;

            if (IsInstalled)
            {
                if (File.Exists(SystemDirectory + @"\OSVersion.data"))
                {
                    FileExistingCount += 1;
                }
                
                if (FileExistingCount == 1)
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

﻿using System;
using System.Management.Automation;
using System.Security.Cryptography;

namespace InvokeIR.PowerForensics.Cmdlets
{

    #region GetHashCommand
    /// <summary> 
    /// This class implements the Get-Hash cmdlet. 
    /// </summary> 

    [Cmdlet(VerbsCommon.Get, "Hash")]
    public class GetHashCommand : PSCmdlet
    {

        #region Parameters

        /// <summary> 
        /// This parameter provides the path for the file
        /// that will be hashed
        /// </summary> 

        [Alias("FilePath")]
        [Parameter(Mandatory = true, Position = 0)]
        public string Path
        {
            get { return filePath; }
            set { filePath = value; }
        }
        private string filePath;

        /// <summary> 
        /// This parameter provides the count of bytes
        /// that should be included when submitted to be hashed.
        /// </summary> 

        [Parameter()]
        [ValidateSet("MD5", "RIPEMD160", "SHA1", "SHA256", "SHA384", "SHA512")]
        public string Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }
        private string algorithm;


        /// <summary> 
        /// This parameter provides the count of bytes
        /// that should be included when submitted to be hashed.
        /// </summary> 

        [Parameter()]
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private int count;

        #endregion Parameters

        #region Cmdlet Overrides

        /// <summary> 
        /// The ProcessRecord method calls the ReadAllBytes method to read the
        /// file into a byte array, which is then passed to the MD5Hash.Get
        /// method to calulate the MD5 hash for the file.
        /// </summary> 
        protected override void ProcessRecord()
        {

            // Read filePath into byte array
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            if (!(this.MyInvocation.BoundParameters.ContainsKey("Algorithm")))
            {
                algorithm = "MD5";
            }

            // If the Count parameter is not used the set count to full size of bytes
            if(!(this.MyInvocation.BoundParameters.ContainsKey("Count")))
            {
                count = bytes.Length;
            }

            //Output the computed MD5 Hash as a string to the PowerShell pipeline
            WriteObject(Hash.Get(bytes, count, algorithm));
            
        } // ProcessRecord 

        protected override void EndProcessing()
        {
            GC.Collect();
        }

        #endregion Cmdlet Overrides

    } // End GetHashCommand class. 

    #endregion GetHashCommand

}
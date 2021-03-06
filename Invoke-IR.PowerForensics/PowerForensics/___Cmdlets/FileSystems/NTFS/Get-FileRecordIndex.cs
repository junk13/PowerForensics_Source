﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using InvokeIR.Win32;

namespace InvokeIR.PowerForensics.Ntfs
{
    #region GetFileRecordIndexCommand
    /// <summary> 
    /// This class implements the Get-FileRecordIndex cmdlet. 
    /// </summary> 

    [Cmdlet(VerbsCommon.Get, "FileRecordIndex")]
    public class FileRecordIndexCommand : Cmdlet
    {

        #region Parameters

        /// <summary> 
        /// This parameter provides the Name of the Volume
        /// for which the FileRecord object should be
        /// returned.
        /// </summary> 

        [Alias("FilePath")]
        [Parameter(Mandatory = true, Position = 0)]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private string path;

        #endregion Parameters

        #region Cmdlet Overrides

        /// <summary> 
        /// The ProcessRecord method calls ManagementClass.GetInstances() 
        /// method to iterate through each BindingObject on each system specified.
        /// </summary> 

        protected override void BeginProcessing()
        {
            NativeMethods.checkAdmin();
        }

        protected override void ProcessRecord()
        {
            WriteObject(IndexEntry.Get(path).RecordNumber);
        } // ProcessRecord 

        protected override void EndProcessing()
        {
            GC.Collect();
        }

        #endregion Cmdlet Overrides

    } // End GetFileRecordIndexCommand class. 
    #endregion GetFileRecordIndexCommand
}

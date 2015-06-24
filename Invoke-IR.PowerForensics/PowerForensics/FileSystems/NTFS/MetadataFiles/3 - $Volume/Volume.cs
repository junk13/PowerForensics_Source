﻿using System;
using System.Text;

namespace InvokeIR.PowerForensics.NTFS
{
    public class Volume
    {
        internal static FileRecord GetFileRecord(string volume)
        {
            return new FileRecord(FileRecord.GetRecordBytes(volume, 3), volume);
        }

        internal static VolumeName GetVolumeNameAttr(FileRecord fileRecord)
        {
            foreach (Attr attr in fileRecord.Attribute)
            {
                if (attr.Name == "VOLUME_NAME")
                {
                    return attr as VolumeName;
                }
            }
            throw new Exception("No VOLUME_NAME attribute found.");
        }

        internal static VolumeInformation GetVolumeInformationAttr(FileRecord fileRecord)
        {
            foreach (Attr attr in fileRecord.Attribute)
            {
                if (attr.Name == "VOLUME_INFORMATION")
                {
                    return attr as VolumeInformation;
                }
            }
            throw new Exception("No VOLUME_INFORMATION attribute found.");
        }
    }
}
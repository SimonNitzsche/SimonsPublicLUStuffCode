using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// <copyright file="Pack.cs">
//  Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Simon Nitzche</author>
// <date>04/01/2018</date>
// <summary>An interface to access the functions of the Pack.dll</summary>
// <license>
//  Do what ever you want with that. Alltho use in own risk. I am not responsible for any damage.
// </license>
// Date is DD/MM/YYYY

public class PACK_DLL_INTERFACE {
    private const string _dll = "Pack.dll";

    public static class Pack {
        /// <summary>
        /// Generates a CRC hash out of a given file.
        /// </summary>
        /// <param name="strFilename">The path to the file</param>
        /// <returns>The CRC hash</returns>
        [DllImport(_dll, EntryPoint = "pack_GetCRCForFilename", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern uint GetCRCForFilename([MarshalAs(UnmanagedType.LPStr)] string strFilename);
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenameCRC"></param>
        /// <param name="packIndex"></param>
        /// <param name="bExists"></param>
        /// <param name="iUncompressedSize"></param>
        /// <param name="uncompressedChecksum"></param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "pack_GetInfoForFile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetInfoForFile(uint filenameCRC, int packIndex, out int bExists, out int iUncompressedSize, out string uncompressedChecksum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenameCRC"></param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "pack_GetPackIndex", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetPackIndex(uint filenameCRC);

        /// <summary>
        /// Looks up the path to the packfile with given index
        /// </summary>
        /// <param name="packIndex">index of the packfile</param>
        /// <param name="strName">Pointer to the final string</param>
        /// <param name="strNameLen">length of the final string</param>
        /// <returns>0 when success.</returns>
        [DllImport(_dll, EntryPoint = "pack_GetPackName", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetPackName(int packIndex, [MarshalAs(UnmanagedType.LPStr)] StringBuilder strName, int strNameLen);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFullFilename"></param>
        /// <param name="strManifestFilename"></param>
        /// <param name="iUncompressedSize"></param>
        /// <param name="iCcompressedSize"></param>
        /// <param name="chkUncompressed"></param>
        /// <param name="chkCompressed"></param>
        /// <param name="fileIsCompressed"></param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "pack_MoveFileToPack", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int MoveFileToPack([MarshalAs(UnmanagedType.LPStr)] string strFullFilename, [MarshalAs(UnmanagedType.LPStr)] string strManifestFilename, int iUncompressedSize,
                                                    int iCcompressedSize, [MarshalAs(UnmanagedType.LPStr)] string chkUncompressed, [MarshalAs(UnmanagedType.LPStr)] string chkCompressed,
                                                    int fileIsCompressed);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packIndex"></param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "pack_PackExists", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int PackExists(int packIndex);

        /// <summary>
        /// Reads the pack catalog and outputs the number of pack files
        /// </summary>
        /// <param name="strPackCatalogName">Path to the catalog</param>
        /// <returns>The count of the pack files</returns>
        [DllImport(_dll, EntryPoint = "pack_ReadPackCatalog", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ReadPackCatalog([MarshalAs(UnmanagedType.LPStr)] string strPackCatalogName);

        /// <summary>
        /// Checks if a pack file is OK
        /// </summary>
        /// <param name="packIndex">The index of th pack file</param>
        /// <returns>0 when file is OK.</returns>
        [DllImport(_dll, EntryPoint = "pack_ReadPackInfoFromFile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int ReadPackInfoFromFile(int packIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDirectory"></param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "pack_SetInstallDir", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetInstallDir([MarshalAs(UnmanagedType.LPStr)] string strDirectory);
    }

    public static class Util {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "util_CheckDiskSpaceFree", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern Int64 CheckDiskSpaceFree();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "util_CheckDiskSpaceUsed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern Int64 CheckDiskSpaceUsed(int option);

        /// <summary>
        /// Deletes files to match maxSize settings.
        /// </summary>
        /// <param name="settings">maxSize in MB</param>
        /// <returns></returns>
        [DllImport(_dll, EntryPoint = "util_DeleteFilesToSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern Int64 DeleteFilesToSettings(Int64 settings);

        // <summary>
        /// Duplicate of util_DeleteFilesToSettings
        /// OBSOLETE!
        /// </summary>
        /// <param name="settings">maxSize in MB</param>
        /// <returns></returns>
        [System.Obsolete("Use util_DeleteFilesToSettings instead.")]
        [DllImport(_dll, EntryPoint = "util_DeleteUGCFiles", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 DeleteUGCFiles(Int64 settings);
    }
}

class Pack {
    public static class Structures {
        public struct PackedFileInfo {
            public int exists;
            public int uncompressedSize;
            public string uncompressedChecksum;
        }
        public struct ManifestEntry {
            public string strManifestFilename;
            public GameDictEntry gameDictEntry;

            public ManifestEntry(string manifest_filename, GameDictEntry game_dict_entry) {
                this.strManifestFilename = manifest_filename;
                this.gameDictEntry = game_dict_entry;
            }
        }
        public struct GameDictEntry {
            public int Asize;
            public string Achecksum;
            public int Bsize;
            public string Bchecksum;

            public GameDictEntry(int sizeA, string checksumA, int sizeB, string checksumB) {
                this.Asize = sizeA;
                this.Achecksum = checksumA;
                this.Bsize = sizeB;
                this.Bchecksum = checksumB;
            }
        }
    }

    public static int SetInstallDir(string strDirectory)
        => PACK_DLL_INTERFACE.Pack.SetInstallDir(strDirectory);

    public static uint GetCRCForFilename(string strFilename)
        => PACK_DLL_INTERFACE.Pack.GetCRCForFilename(strFilename);

    public static int GetPackIndex(uint filenameCRC)
        => PACK_DLL_INTERFACE.Pack.GetPackIndex(filenameCRC);

    public static Structures.PackedFileInfo GetInfoForFile(uint filenameCRC, int packIndex) {
        Structures.PackedFileInfo packedFileInfo;

        packedFileInfo.uncompressedChecksum = new string(Char.MinValue, 33);

        int retVal = PACK_DLL_INTERFACE.Pack.GetInfoForFile(filenameCRC, packIndex, out packedFileInfo.exists, out packedFileInfo.uncompressedSize, out packedFileInfo.uncompressedChecksum);

        return packedFileInfo;
    }

    public static int MoveFileToPack(string strFullFilename, Structures.ManifestEntry manifest_entry, int fileIsCompressed) {
        return PACK_DLL_INTERFACE.Pack.MoveFileToPack(
            strFullFilename,
            manifest_entry.strManifestFilename,
            manifest_entry.gameDictEntry.Bsize,
            manifest_entry.gameDictEntry.Asize,
            manifest_entry.gameDictEntry.Bchecksum,
            manifest_entry.gameDictEntry.Achecksum,
            fileIsCompressed
        );
    }

    public static int ReadPackInfoFromFile(int packIndex)
        => PACK_DLL_INTERFACE.Pack.ReadPackInfoFromFile(packIndex);

    public static string[] ReadPackCatalog(string strPackCatalogName) {
        List<string> packfileNames = new List<string>();
        int packFileCount = PACK_DLL_INTERFACE.Pack.ReadPackCatalog(strPackCatalogName);

        Console.WriteLine(format: "Found {0} Packs", arg0: packFileCount);

        const int bufferLen = 256;
        string emptyBuffer = new String(char.MinValue, bufferLen);

        for(int i = 0; i < packFileCount; i++) {
            StringBuilder s = new StringBuilder(emptyBuffer);
            if( PACK_DLL_INTERFACE.Pack.GetPackName(i, s, s.Capacity) == 0) {
                packfileNames.Add(s.ToString());
            }
        }

        return packfileNames.ToArray();
    }

    public static bool PackExists(int packIndex)
        => PACK_DLL_INTERFACE.Pack.PackExists(packIndex) != 0;

    public static Int64 CheckFreeDiskSpace()
        => PACK_DLL_INTERFACE.Util.CheckDiskSpaceFree();

    public static Int64 CheckDiskSpaceUsed(int option)
        => PACK_DLL_INTERFACE.Util.CheckDiskSpaceUsed(option);

    public static Int64 DeleteUGCFiles(Int64 maxDiskSpaceAllowed) {
        Console.WriteLine(format: "DeleteUGCFiles deleting {0} GBs", arg0: maxDiskSpaceAllowed);
        return PACK_DLL_INTERFACE.Util.DeleteFilesToSettings(maxDiskSpaceAllowed * 1024);
    }
}

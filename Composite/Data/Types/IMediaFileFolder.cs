﻿using System;
using System.Linq;
using Composite.Data.Hierarchy;


namespace Composite.Data.Types
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    [Title("C1 Media Folder")]
    [KeyPropertyName("KeyPath")]
    [DataAncestorProviderAttribute(typeof(MediaFileDataAncesorProvider))]
    [DataScope(DataScopeIdentifier.PublicName)]
    [ImmutableTypeId("{76C4D9D8-2558-4475-801B-FB56C5E923A3}")]
    [LabelPropertyName("CompositePath")]
    [RelevantToUserType(UserType.Developer)]
    public interface IMediaFileFolder : IData
    {
        /// <exclude />
        [StoreFieldType(PhysicalStoreFieldType.Guid)]
        [ImmutableFieldId("{84c8046c-a53c-42dd-bd54-b64c6f7511c1}")]
        Guid Id { get; }


        /// <exclude />
        [StoreFieldType(PhysicalStoreFieldType.String, 2048)]
        [ImmutableFieldId("{e03e1acb-b5bd-4354-bfd4-5e2626381d82}")]
        string KeyPath { get; }


        /// <exclude />
        [ImmutableFieldId("{ADB2660D-BAB3-499a-AE12-50AA703FA3B0}")]
        [StoreFieldType(PhysicalStoreFieldType.String, 2048)]
        string CompositePath { get; set; }


        /// <exclude />
        [ImmutableFieldId("{814D45C9-D424-4420-8DBB-3F93E4EF24E2}")]
        [StoreFieldType(PhysicalStoreFieldType.String, 32)]
        string StoreId { get; set; }


        /// <exclude />
        [ImmutableFieldId("{A71332BC-F6E5-4e1b-8BB6-7C6AA57BECC6}")]
        [StoreFieldType(PhysicalStoreFieldType.String, 2048)]
        string Path { get; set; }


        /// <exclude />
        [ImmutableFieldId("{E4DBB69F-B1F6-46a1-A8A6-BDDD4CB344D6}")]
        [StoreFieldType(PhysicalStoreFieldType.String, 256)]
        string Title { get; set; }


        /// <exclude />
        [ImmutableFieldId("{51CF6EFA-66C3-413e-9FCD-06EA52871182}")]
        [StoreFieldType(PhysicalStoreFieldType.LargeString)]
        string Description { get; set; }


        /// <exclude />
        [ImmutableFieldId("{FA03F9D5-C8AF-469c-BC02-F11118D21A0F}")]
        [StoreFieldType(PhysicalStoreFieldType.Boolean)]
        bool IsReadOnly { get; }
    }



    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public static class IMediaFileFolderUtils
    {
        /// <summary>
        /// Creates a folder path given folder path and the name of the folder
        /// </summary>
        /// <param name="parentMediaFolder"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static string CreateFolderPath(this IMediaFileFolder parentMediaFolder, string folderName)
        {
            return CreateFolderPath(parentMediaFolder.Path, folderName);
        }



        /// <summary>
        /// Creates a folder path given folder path and the name of the folder
        /// </summary>
        /// <param name="parentFolderPath"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static string CreateFolderPath(string parentFolderPath, string folderName)
        {
            string folderPath;
            if (parentFolderPath == "/")
            {
                folderPath = parentFolderPath + folderName;
            }
            else
            {
                folderPath = parentFolderPath + "/" + folderName;
            }

            folderPath = folderPath.Replace('\\', '/');
            while (folderPath.Contains("//"))
            {
                folderPath = folderPath.Replace("//", "/");
            }

            if (!folderPath.StartsWith("/"))
            {
                folderPath = "/" + folderPath;
            }

            if (folderPath.EndsWith("/"))
            {
                folderPath = folderPath.Remove(folderPath.Length - 1);
            }

            return folderPath;
        }        



        /// <summary>
        /// Returns the parent folder for the given media folder
        /// </summary>
        /// <param name="mediaFileFolder"></param>
        /// <returns></returns>
        public static string GetParentFolderPath(this IMediaFileFolder mediaFileFolder)
        {
            return GetParentFolderPath(mediaFileFolder.Path);
        }



        /// <summary>
        /// Returns the parent folder for the given media folder path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentFolderPath(string path)
        {
            if (path == "/")
            {
                return path;
            }

            string parentPath = path.Substring(0, path.LastIndexOf("/"));
            if (parentPath == "")
            {
                return "/";
            }

            return parentPath;
        }



        /// <summary>
        /// Returns true if the given media folder exists
        /// </summary>
        /// <param name="mediaFileFolder"></param>
        /// <returns></returns>
        public static bool DoesFolderExists(this IMediaFileFolder mediaFileFolder)
        {
            return DoesFolderExists(mediaFileFolder.Path);
        }



        /// <summary>
        /// Returns true if the given media folder path exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DoesFolderExists(string path)
        {
            if (path == "/")
            {
                return true;
            }

            using (DataConnection dataConnection = new DataConnection())
            {
                return (from item in dataConnection.Get<IMediaFileFolder>()
                              where item.Path == path
                              select item).Any();
            }
        }



        /// <summary>
        /// Returns true if the given media folders parent folder exists
        /// </summary>
        /// <param name="mediaFileFolder"></param>
        /// <returns></returns>
        public static bool DoesParentFolderExists(this IMediaFileFolder mediaFileFolder)
        {
            return DoesFolderExists(mediaFileFolder.GetParentFolderPath());
        }
    }
}

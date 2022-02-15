﻿using System;
using UltimateMovieNight.Data.VO;

namespace UltimateMovieNight.Business
{
	public interface IFileBusiness
	{
		public byte[] GetFile(string filename);
		public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
		public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
	}
}


using System.IO;
using System.Threading;

namespace Adamant.Exploratory.Common
{
	public static class DirectoryUtil
	{
		public static void SafeDelete(string path)
		{
			for(var i = 0; i < 3; i++)
			{
				try
				{
					if(Directory.Exists(path))
					{
						Directory.Delete(path, true);
						// Having problems with creating dir immediately after deleting
						while(Directory.Exists(path))
							Thread.Sleep(1);
					}
					return; // if no error, don't return
				}
				catch(IOException)
				{
					// Ignore, we want to retry
				}
			}
		}
	}
}

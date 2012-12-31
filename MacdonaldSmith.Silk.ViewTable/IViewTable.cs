using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public interface IViewTable
	{
		void Clear();
		
		void AddStringColumn(string columnName);
		
		void DeleteColumn(string columnName);
		
		void ReSize(int rowCount);
		
		void Update (int colIndex, string value);
		
		void Update (string colName, string value);
		
		void Commit();
	}
}


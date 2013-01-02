using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public interface IViewTable
	{
		void Clear();

	    void AddColumn<T>(string columnName);

	    void AddColumn<T>(string columnName, T defaultValue);
		
		void DeleteColumn(string columnName);
		
		void ReSize(int rowCount);
		
		void Update<T> (int rowIndex, int columnIndex, T value);
		
		void Update<T> (int rowIndex, string colName, T value);

	    T GetValue<T>(int rowIndex, string columnName);

	    T GetValue<T>(int rowIndex, int columnIndex);

		void Commit();

	    void DropChanges();

        int RowCount { get; }

        int ColumnCount { get; }
	}
}


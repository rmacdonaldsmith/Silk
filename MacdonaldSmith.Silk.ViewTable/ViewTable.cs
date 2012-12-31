using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public class ViewTable
	{
		private int _rowCount = 0;
		private int _usedRowCount = 0;
		private byte[] _bitMaskColumn;
		private string[] _columnNames;
		private ColumnBase[] _columns;
		
		public ViewTable(int rowCount)
		{
			_rowCount = rowCount;
			_bitMaskColumn = new byte[_rowCount];
			_columnNames = new string[_rowCount];
		}
		
		public void Clear ()
		{
			foreach(ColumnBase column in _columns)
			{
				column.Clear();
			}
		}
		
		
		public void AddStringColumn (string columnName)
		{
			StringColumn stringColumn = new StringColumn(_rowCount, columnName);
		}
		
		
		public void DeleteColumn (string columnName)
		{
			throw new System.NotImplementedException();
		}
		
		
		public void ReSize (int rowCount)
		{
			throw new System.NotImplementedException();
		}
		
		
		public void Update (int colIndex, string value)
		{
			throw new System.NotImplementedException();
		}
		
		
		public void Update (string colName, string value)
		{
			throw new System.NotImplementedException();
		}
		
		
		public void Commit ()
		{
			throw new System.NotImplementedException();
		}
	}
}


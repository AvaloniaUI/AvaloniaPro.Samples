## FlatTreeDataGridSample

This sample demonstrates how to use the `TreeDataGrid` component to display data in a flat "DataGrid"-like layout.

It uses the MVVM pattern and sets up a `FlatTreeDataGridSource` in `MainWindowViewModel` which is exposed to the view via a property.

This `FlatTreeDataGridSource` sets up a grid which displays a list of countries along with various details about them.

## Key Features Demonstrated

### Column Configuration
- **Multiple Column Types**: The sample shows how to define different column types:
  - A row header column for row numbers. Together with the `FrozenColumnCount` property, this keeps the row numbers visible during horizontal scrolling.
  - Read/write text columns with the `Country` name column
  - Template columns with custom cell and edit templates for the `Region` column
  - Read-only text columns for data like `Population` and `Area`
  - Customized text columns with alignment and size constraints for the `GDP` column

### Advanced Grid Features
- **Text Search**: Enabling text search functionality for specific columns
- **Custom Data Templates**: Demonstrating how to use data templates for displaying and editing cell content
- **Row Drag & Drop**: Auto drag-drop functionality for rows with `AutoDragDropRows="True"`
- **Three-state Sorting**: Enabling three-state sorting (ascending/descending/none) with `AllowTriStateSorting="True"`

### Styling & Customization
- **Alternating Row Colors**: Implementing alternating background colors for rows using `RowTheme`
- **Column Header Styling**: Custom styling for specific column headers (last column appears in bold)
- **Cell Styling**: Custom styling for specific cells (last column cells appear in bold)
- **Custom Templates**: Both display and edit templates for the Region column, including a ComboBox for selection
- **Frozen Columns**: Keeping certain columns fixed during horizontal scrolling by setting `TreeDataGrid.FrozenColumnCount`

### Data Binding
- Proper MVVM implementation showing how to bind a collection of domain objects (Countries) to the TreeDataGrid
- Clean separation of concerns between data (Country model), presentation logic (MainWindowViewModel), and UI (MainWindow)

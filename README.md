# Mobile UI for Xamarin.Forms: Error Monitoring Client App

This example demonstrates how to implement a mobile client for an automated application monitoring and crash reporting system. The application uses sample data and contains the following pages:

- [Reports](./CS/LogifyMobile/LogifyMobile/Views/Reports) - Displays a list of crash reports/exception events.
- [Exception Report](./CS/LogifyMobile/LogifyMobile/Views/ReportDetails) – This screen is activated when a user taps an item on the Reports list and displays detailed information on the selected exception/crash event.
- [Monitored Apps](./CS/LogifyMobile/LogifyMobile/Views/Apps) - Displays a list of all tracked/monitored applications.
- [App Details](./CS/LogifyMobile/LogifyMobile/Views/AppDetails) – Displays detailed information on a monitored application. 
- [Statistics](./CS/LogifyMobile/LogifyMobile/Views/Reports) – Allows users to inspect the overall “health” of an application. 

This application uses the following DevExpress Xamarin.Forms controls:
- [TabPage](https://docs.devexpress.com/MobileControls/401160/xamarin-forms/navigation-controls/tab-page/index) - Implements the application's main navigation.
- [TabView](https://docs.devexpress.com/MobileControls/401161/xamarin-forms/navigation-controls/tab-view/index) – Implements tab navigation between information blocks on the exception report screen.
- [DrawerPage](https://docs.devexpress.com/MobileControls/401159/xamarin-forms/navigation-controls/drawer-page/index) – Implements a filter panel that allows users to filter exception reports and modify the subscription and team memberships. 
- [DataGridView](https://docs.devexpress.com/MobileControls/400543/xamarin-forms/data-grid/index) - Displays information on app pages. The following grid features are used: [template column](https://docs.devexpress.com/MobileControls/DevExpress.XamarinForms.DataGrid.TemplateColumn), data [sorting](https://docs.devexpress.com/MobileControls/400552/xamarin-forms/data-grid/getting-started/lesson-5-sort-data) and [grouping](https://docs.devexpress.com/MobileControls/400550/xamarin-forms/data-grid/getting-started/lesson-3-group-data), [load more](https://docs.devexpress.com/MobileControls/400997/xamarin-forms/data-grid/examples/load-more) and [swipe actions](https://docs.devexpress.com/MobileControls/401053/xamarin-forms/data-grid/examples/swipe-actions). 
- [Charts](http://docs.devexpress.com/MobileControls/400422/xamarin-forms/charts/index) - Visualize data on the Statistics screen.

The following blog posts describe the process of designing and developing this mobile app:  
[Building the Error Monitoring Client App](https://community.devexpress.com/tags/Error+Monitoring+Client+App/default.aspx)

To run the application:
1. [Obtain your NuGet feed URL](http://docs.devexpress.com/GeneralInformation/116042/installation/install-devexpress-controls-using-nuget-packages/obtain-your-nuget-feed-url).
2. Register the DevExpress NuGet feed as a package source.
3. Restore all NuGet packages for the solution.

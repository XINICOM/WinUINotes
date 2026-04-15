using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using WinUINotes.Bus.ViewModels;
using WinUINotes.Tests.Fakes;

namespace WinUINotes.Tests
{
    [TestClass]
    public partial class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(0 , 0);
        }


        // Use the UITestMethod attribute for tests that need to run on the UI thread.
        //[UITestMethod]
        ////[TestMethod]
        //public void TestMethod2()
        //{
        //    var grid = new Grid();
        //    Assert.AreEqual(0 , grid.MinWidth);

        //    //Grid grid = null;

        //    //// 手动调度到 UI 线程
        //    //await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
        //    //    CoreDispatcherPriority.Normal ,
        //    //    () =>
        //    //    {
        //    //        grid = new Grid();
        //    //    });

        //    //Assert.AreEqual(0 , grid.MinWidth);
        //}

        [TestMethod]
        public void TestCreateUnsavedNote()
        {
            var noteVm = new NoteViewModel(new FakeFileService());
            Assert.IsNotNull(noteVm);
            Assert.IsGreaterThan(DateTime.Now.AddHours(-1) , noteVm.Date);
            Assert.EndsWith(".tant" , noteVm.Filename);
            Assert.StartsWith("notes" , noteVm.Filename);
            noteVm.Text = "Sample Note";
            Assert.AreEqual("Sample Note" , noteVm.Text);
            noteVm.SaveCommand.Execute(null);
            Assert.AreEqual("Sample Note" , noteVm.Text);
        }
    }
}

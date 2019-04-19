using Castle.Core.Logging;
using NUnit.Framework;
using System;
using System.Windows;
using System.Windows.Automation;
using TestStack.White.Configuration;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;

namespace TestStack.White.UnitTests
{
    [TestFixture]
    public class SpanTests
    {
        [Test]
        public void UnionTest()
        {
            var verticalSpan = new VerticalSpan(new Rect(10, 10, 0, 10)).Union(new Rect(10, 5, 0, 10));
            Assert.That(verticalSpan.DoesntContain(new Rect(10, 10, 0, 5)), Is.False);
            Assert.That(verticalSpan.DoesntContain(new Rect(10, 10, 0, 10)), Is.False);
        }

        [Test]
        public void CutTest()
        {
            var verticalSpan = new VerticalSpan(new Rect(10, 10, 10, 10)).Minus(new Rect(10, 15, 5, 5));
            Assert.That(verticalSpan.DoesntContain(new Rect(10, 10, 10, 12)), Is.True);
            Assert.That(verticalSpan.DoesntContain(new Rect(10, 10, 10, 4)), Is.False);
        }

        [Test]
        public void EmptyIsOutsideTest()
        {
            var verticalSpan = new VerticalSpan(new Rect(10, 10, 10, 10));
            Assert.That(verticalSpan.DoesntContain(Rect.Empty), Is.True);
        }

        [Test]
        public void HelloTest()
        {
            CoreAppXmlConfiguration.Instance.LoggerFactory = new ConsoleFactory(LoggerLevel.Debug);
            CoreAppXmlConfiguration.Instance.RawElementBasedSearch = true;
            var applicationName = "notepad.exe";
            Application application = Application.Launch(applicationName);
            var window = application.GetWindow("Untitled - Notepad", InitializeOption.NoCache);
            var menubar = window.MenuBars[1];
            var file = menubar.MenuItem("File");
            file.Click();
            file.DrawHighlight();
            var x = window.Get(SearchCriteria.ByControlType(ControlType.Menu));
            //var x = window.Get(SearchCriteria.ByText("File"));
            //Console.WriteLine("======================");
            //Console.WriteLine(x.Name);
            //Console.WriteLine(x.AutomationElement.Current.ClassName);
            //var x = window.Popup;
            //x.DrawHighlight();
            //var file_popup = window.Get<PopUpMenu>(SearchCriteria.ByText("File"));
            //file_popup.DrawHighlight();

        }
    }
}
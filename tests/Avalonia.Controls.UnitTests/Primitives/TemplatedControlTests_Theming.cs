using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using Avalonia.UnitTests;
using Xunit;

namespace Avalonia.Controls.UnitTests.Primitives
{
    public class TemplatedControlTests_Theming
    {
        [Fact]
        public void Theme_Is_Applied_When_Attached_To_Logical_Tree()
        {
            using var app = UnitTestApplication.Start(TestServices.RealStyler);
            var target = new ThemedControl
            {
                Theme = CreateTheme(),
            };

            Assert.Null(target.Template);

            var root = new TestRoot(target);

            Assert.NotNull(target.Template);
        }

        [Fact]
        public void Theme_Is_Detached_When_Theme_Property_Changed()
        {
            using var app = UnitTestApplication.Start(TestServices.RealStyler);
            var target = new ThemedControl();
            var root = new TestRoot(target);

            target.Theme = CreateTheme();

            Assert.Null(target.Template);
        }

        private static ControlTheme CreateTheme()
        {
            var template = new FuncControlTemplate<ThemedControl>((o, n) =>
                new Border { Name = "PART_Border" });

            return new ControlTheme
            {
                TargetType = typeof(ThemedControl),
                Setters =
                {
                    new Setter(ThemedControl.TemplateProperty, template),
                }
            };
        }

        private class ThemedControl : TemplatedControl
        {
        }
    }
}

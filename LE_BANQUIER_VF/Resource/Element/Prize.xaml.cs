using LE_BANQUIER_VF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.Resource.Element
{
    /// <summary>
    /// Interaction logic for Prize.xaml
    /// </summary>
    public partial class Prize : UserControl
    {
        public Prize()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PrizeProperty =
            DependencyProperty.Register("PrizeElement", typeof(Model.Prize), typeof(Prize),
                new PropertyMetadata(null, OnPrizeChanged));

        public Model.Prize PrizeElement
        {
            get => (Model.Prize)GetValue(PrizeProperty);
            set => SetValue(PrizeProperty, value);
        }

        private static void OnPrizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Prize;

            if (e.OldValue is Model.Prize oldPrize)
                oldPrize.PropertyChanged -= control.OnPrizeStateChanged;

            if (e.NewValue is Model.Prize newPrize)
                newPrize.PropertyChanged += control.OnPrizeStateChanged;
        }

        private void OnPrizeStateChanged(object sender, EventArgs e)
        {
            if (! PrizeElement.IsAvailable)
            {
                // Play explosion animation for eliminated prizes
                PlayFlipAnimation();
                PlayExplosionAnimation();
            }
        }

        private void PlayFlipAnimation()
        {
            var flipAnimation = new DoubleAnimation
            {
                From = 0,
                To = 180,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                BeginTime = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            var transform = new RotateTransform();
            this.RenderTransform = transform;
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            transform.BeginAnimation(RotateTransform.AngleProperty, flipAnimation);
        }

        private void PlayExplosionAnimation()
        {
            var scaleUpAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.5,
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                AutoReverse = true,
                BeginTime = TimeSpan.FromSeconds(3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var transform = new ScaleTransform();
            this.RenderTransform = transform;
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            transform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleUpAnimation);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleUpAnimation);
        }
    }
}

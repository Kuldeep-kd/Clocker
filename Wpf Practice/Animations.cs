using System;
using System.Windows;
using System.Windows.Media.Animation;

public class Animations
{
    //FadeIn Animation
    public DoubleAnimation FrwdAnim = new DoubleAnimation()
    {
        From = 1.0,
        To = 0.0,
        Duration = new Duration(TimeSpan.FromSeconds(1)),
        AccelerationRatio = 0.2,
        DecelerationRatio = 0.5
    };
    //FadeOut Animation
    public DoubleAnimation RevAnim = new DoubleAnimation()
    {
        From = 0.0,
        To = 1.0,
        Duration = new Duration(TimeSpan.FromSeconds(1)),
        AccelerationRatio = 0.2,
        DecelerationRatio = 0.5
    };
    //Height Increase Animation
    public DoubleAnimation HtIncAnim = new DoubleAnimation()
    {
        From = 550,
        To = 630,
        Duration = new Duration(TimeSpan.FromSeconds(0.3)),
        AccelerationRatio = 0.1,
        DecelerationRatio = 0.1
    };

    //Height Decrease Animation
    public DoubleAnimation HtDecAnim = new DoubleAnimation()
    {
        From = 630,
        To = 550,
        Duration = new Duration(TimeSpan.FromSeconds(0.3)),
        AccelerationRatio = 0.1,
        DecelerationRatio = 0.1
    };

    //StartUp Animation Increase Animation
    public DoubleAnimation StartAnimation = new DoubleAnimation()
    {
        From = 0,
        To = 520,
        Duration = new Duration(TimeSpan.FromSeconds(1.5)),
        AccelerationRatio = 0.5,
        DecelerationRatio = 0.5
    };
    public DoubleAnimation ExitAnimation = new DoubleAnimation()
    {
        From = 550,
        To = 0,
        Duration = new Duration(TimeSpan.FromSeconds(0.5)),
        AccelerationRatio = .8,
        DecelerationRatio = .2
    };
}

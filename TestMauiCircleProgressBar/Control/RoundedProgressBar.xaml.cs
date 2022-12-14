namespace TestMauiCircleProgressBar.Control;

public partial class RoundedProgressBar : ContentView
{
    public static readonly BindableProperty ProgressProperty = BindableProperty.Create("Progress", typeof(double),
        typeof(RoundedProgressBar), propertyChanged: ProgressPropertyChanged, defaultValue: 0.0);

    static void ProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        ((RoundedProgressBar)bindable).SetValue(ProgressProperty, newValue);
    }

    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create("ProgressColor", typeof(Color),
        typeof(RoundedProgressBar), propertyChanged: ProgressColorPropertyChanged, defaultValue: Colors.White);

    static void ProgressColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.ProgressColor = (Color)newValue;
        ((RoundedProgressBar)bindable).SetValue(ProgressColorProperty, newValue);
    }

    public static readonly BindableProperty PathProgressColorProperty = BindableProperty.Create("PathProgressColor", typeof(Color),
        typeof(RoundedProgressBar), propertyChanged: PathProgressColorPropertyChanged, defaultValue: Colors.DarkGray);

    static void PathProgressColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.PathProgressColor = (Color)newValue;
        ((RoundedProgressBar)bindable).SetValue(PathProgressColorProperty, newValue);
    }

    public static readonly BindableProperty TextProgressColorProperty = BindableProperty.Create("TextProgressColor", typeof(Color),
        typeof(RoundedProgressBar), propertyChanged: TextProgressColorPropertyChanged, defaultValue: Colors.White);

    static void TextProgressColorPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.TextProgressColor = (Color)newValue;
        ((RoundedProgressBar)bindable).SetValue(TextProgressColorProperty, newValue);
    }

    public static readonly BindableProperty ProgressSizeProperty = BindableProperty.Create("ProgressSize", typeof(int),
        typeof(RoundedProgressBar), propertyChanged: ProgressSizePropertyChanged, defaultValue: 30);

    static void ProgressSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.ProgressSize = (int)newValue;
        ((RoundedProgressBar)bindable).SetValue(ProgressSizeProperty, newValue);
    }

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double),
        typeof(RoundedProgressBar), propertyChanged: FontSizePropertyChanged, defaultValue: 12.0);

    static void FontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.FontSize = (double)newValue;
        ((RoundedProgressBar)bindable).SetValue(FontSizeProperty, newValue);
    }

    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create("FontAttributes", typeof(FontAttributes),
        typeof(RoundedProgressBar), propertyChanged: FontAttributesPropertyChanged, defaultValue: FontAttributes.None);

    static void FontAttributesPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.FontAttributes = (FontAttributes)newValue;
        ((RoundedProgressBar)bindable).SetValue(FontAttributesProperty, newValue);
    }

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string),
        typeof(RoundedProgressBar), propertyChanged: FontFamilyPropertyChanged, defaultValue: string.Empty);

    static void FontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.FontFamily = (string)newValue;
        ((RoundedProgressBar)bindable).SetValue(FontFamilyProperty, newValue);
    }

    public static readonly BindableProperty ProgressFillProperty = BindableProperty.Create("ProgressFill", typeof(Brush),
        typeof(RoundedProgressBar), propertyChanged: ProgressFillPropertyChanged, defaultValue: Brush.Transparent);

    static void ProgressFillPropertyChanged(BindableObject bindable, object oldValue, object newValue) 
    {
        RoundedProgressBar progressBar = (RoundedProgressBar)bindable;
        progressBar.ProgressFill = (Brush)newValue;
        ((RoundedProgressBar)bindable).SetValue(FontFamilyProperty, newValue);
    }

    public Brush ProgressFill
    {
        get => (Brush)GetValue(ProgressFillProperty);
        set
        {
            SetValue(ProgressFillProperty, value);
            PathProgress.Fill= value;
        }
    }

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set
        {
            SetValue(FontFamilyProperty, value);
            ProgressLabel.FontFamily = value;
        }
    }

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set
        {
            SetValue(FontAttributesProperty, value);
            ProgressLabel.FontAttributes = value;
        }
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set
        {
            SetValue(FontSizeProperty, value);
            ProgressLabel.FontSize = value;
        }
    }

    public int ProgressSize
    {
        get => (int)GetValue(ProgressSizeProperty);
        set
        {
            SetValue(ProgressSizeProperty, value);
            ProgressBorder.WidthRequest = value*2+15;
            ProgressBorder.HeightRequest= value*2+15;
            StartPoint.StartPoint = new Point(value + 1, 1);
            PathProgress.WidthRequest = value * 2 + 4;
            PathProgress.HeightRequest = value * 2 + 4;
            SizeOfProgress = new Size(value, value);
            ArcSegment.Size = SizeOfProgress ;
        }
    }

    public Color TextProgressColor
    {
        get => (Color)GetValue(TextProgressColorProperty);
        set
        {
            SetValue(TextProgressColorProperty, value);
            ProgressLabel.TextColor = value;
        }
    }

    public Color PathProgressColor
    {
        get => (Color)GetValue(PathProgressColorProperty);
        set
        {
            SetValue(PathProgressColorProperty, value);
            PathProgress.Stroke = value;
        }
    }

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set
        {
            SetValue(ProgressColorProperty, value);
            ProgressLine.Stroke = value;
        }
    }

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set
        {
            SetValue(ProgressProperty, value);
            CalculateArc();
        }
    }

    private Size SizeOfProgress;

    public RoundedProgressBar()
	{
		InitializeComponent();
        SizeOfProgress.Width = 30;
        ProgressLabel.FontSize = 12;
    }

  

    private void CalculateArc()
    {
        var radius = (int)SizeOfProgress.Width;
        int deltaX = (int)SizeOfProgress.Width+1;
        int deltaY = (int)SizeOfProgress.Width+1;
        if (Progress == 0)
        {
            ProgressLabel.Text = $"{Convert.ToInt32(Progress)}%";
            ProgressLine.IsVisible = false;
            return;
        }
        
        double x =0,y = 0;
        ProgressLabel.Text = $"{Convert.ToInt32(Progress)}%";
        var _angle = (Progress-.01)* 360 / 100;
        double angle = Math.PI * _angle / 180.0;
        if (_angle <= 90)
        {
            ArcSegment.IsLargeArc = false;
            x = Math.Abs(radius  * Math.Sin(angle)+deltaX );
            y = Math.Abs(Math.Abs(radius  * Math.Cos(angle))-deltaY);
        }
        else if (_angle > 90 &&_angle < 180)
        {
            ArcSegment.IsLargeArc = false;
            x = Math.Abs(radius  * Math.Sin(angle)+deltaX );
            y = Math.Abs(Math.Abs(radius  * Math.Cos(angle))+deltaY);
        }
        else if (_angle >= 180 && _angle < 270)
        {
            ArcSegment.IsLargeArc = true;
            x = Math.Abs(radius  * Math.Sin(angle) + deltaX);
            y = Math.Abs(radius  * Math.Cos(angle))+deltaY;
        }
        else if (_angle >= 270)
        {
            ArcSegment.IsLargeArc = true;
            x = Math.Abs(radius  * Math.Sin(angle)+deltaX );
            y = Math.Abs(Math.Abs(radius  * Math.Cos(angle))-deltaY);
        }

        /*xLabel.Text = $"x={x.ToString("N")}";
        yLabel.Text = $"y={y.ToString("N")}";*/
        ArcSegment.Point = new Point(x, y);
        ProgressLine.IsVisible=true;
    }
}
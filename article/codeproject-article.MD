# Locus Effects
Published: February 15th, 2006

Text and images: [Yuval Naveh](https://www.linkedin.com/in/yuvalnaveh/)

## Overview

Locus effects is a .NET/C# visual effects framework for changing the user's locus of attention to an area on the screen.
Example 1:

![Example 1](https://github.com/bigman73/locus-effects/blob/master/images/LocusEffectsIntro.gif?raw=true)

Example 2:

![Example 2](https://github.com/bigman73/locus-effects/blob/master/images/LocusEffectsIntro2.gif?raw=true)

Example 3:

![Example 3](https://github.com/bigman73/locus-effects/blob/master/images/LocusEffectsIntro3.gif?raw=true)

## Introduction
I was first introduced to the term "Locus" almost an year ago, when I attended a fascinating 2-day seminar with Prof. Jim Coplien on the subject of “Humane User Interfaces”. It was quite amazing to know that the design of user interfaces in software really hasn’t got that much attention and has a long way to go. Anyway, one of the key aspects in designing a good user interface is to understand how the human perception works. First of all, we humans have only one conscious (well, perhaps except for some lunatics ;)). In our mind, the thing that gets high conscious attention or most of our conscious attention is called the "Locus of attention".

> Locus- Latin for "the place."

So our Locus is the place where our conscious/mind is set. It is the state of our mind. The Locus can change if an external event alarms our mind. For example: when we are reading a book, if a rapid ball of fire moves from right to left in the horizon, it is likely to catch our attention. This biological mechanism is built in us for survival. If a dangerous event happens, our mind pays attention to it since it is something that can kill us, harm us etc.

It is obvious that our Locus can be changed. This can be taken into consideration while designing a user interface that requires user’s attention. But usually it is misused or overly used; causing distracted user interfaces that makes us unproductive and tired. Modal forms attract our locus but since they are so repetitive and since they block our work we tend to click OK automatically and close these nags. Locus changes should be done as and when needed with great care and thought. If the locus changes for a brief time, our productivity is not harmed but after that we loose concentration in our previous task. Switching back to the previous task is quite slow and causes productivity problems.

### Why do we need Locus Effects?
It is quite evident from the introduction and from daily experience that to change or attract a user’s attention we need to perform some sort of alarming event. This event can be a sound (clap your hands in a room and everyone will look at you), a visual effect (in the same room wave with a red handkerchief) or a physical sensation (somebody touching your shoulder while you are staring at visual studio).

I want to provide in this article a means to perform visual Locus Effects (maybe in future I might write a version where the computer touches your shoulder ;)).

We need Locus Effects if we want to draw the user’s attention temporarily to some activity that is being done without harming productivity, while making this change pleasant.

A good example of an application that uses the concept of Locus Effects is shown in DevExpress’ CodeRush. This highly productive commercial Visual Studio .NET add-on uses a cool set of effects in order to draw your attention to changes. For example: After setting a bookmark (Alt+Home) if you go somewhere else in the file and click Esc, the cursor returns to the bookmark and shows the original cursor position using an effect that looks like a radar beacon.

Another example for a locus effect (again a beacon) is Microsoft's mouse pointer highlight feature - If you turn it on and press Ctrl a (not so nice..) beacon draws your attention to the current position of the mouse. (Control Panel/Mouse/Pointer Options/Show location of pointer..)

So Locus Effects can be used in different scenarios where setting the attention of the user can help in improving productivity. Sample scenarios:

* Showing a position in a text editor (after find, find and replace, etc.).
* Showing bookmarks in a text editor.
* Showing a certain control that needs to be clicked.
* Showing a validation error or warning on the form that needs user changes.

## Using the framework (or, How do I show a Locus Effect?)
Using the LocusEffects framework is quite straightforward and easy. First, add the LocusEffectsProvider component to VS components toolbox by:

1. Show tool box.
2. Add/Remove Items....
3. Browse and select the assembly BigMansStuff.LocusEffects.dll.
4. Select the component: LocusEffectsProvider.
5. Select OK.

Now drag and drop LocusEffectsProvider to your form, user control or component.

### Initialization
Assuming we dragged a LocusEffectsProvider component and named it "locusEffectsProvider": In the load event handler, or in another initialization event, initialize the component by calling:

```csharp
locusEffectsProvider.Initialize();
```

### Showing a predefined Locus Effect
If a certain predefined effect is good enough for the job and needs no customizations, simple show it by calling locusEffectsProvider.ShowLocusEffect. For example showing the predefined arrow Locus Effect:

```csharp
locusEffectsProvider.ShowLocusEffect( this,
    this.RectangleToScreen( locusArea.Bounds ),
    LocusEffectsProvider.DefaultLocusEffectArrow );
```

### Customizations: Creating and showing a custom Locus Effect
If customization is needed, create an instance of a predefined Locus Effect class, change some of its properties and register it once (code snippets from test application showing creation of a custom curved arrow Locus Effect):


```csharp
private void InitializeLocusEffects()
{
    locusEffectsProvider.Initialize() ;

    this.CreateCustomLocusEffects();
}

private void CreateCustomLocusEffects()
{
    ResourceManager rm = new ResourceManager(
        "BigMansStuff.TestLocusEffects.Images.CustomImages",
        Assembly.GetExecutingAssembly() ) ;

    m_customArrowLocusEffect = new ArrowLocusEffect() ;
    m_customArrowLocusEffect.Name = "CustomArrow_Curved" ;
    m_customArrowLocusEffect.AnimationStartColor = Color.Red ;
    m_customArrowLocusEffect.AnimationEndColor = Color.Yellow ;
    m_customArrowLocusEffect.Bitmap = rm.GetObject(
        "CustomCurvedArrowBitmap" ) as Bitmap ;
    locusEffectsProvider.AddLocusEffect( m_customArrowLocusEffect ) ;
. . .
}
```
Finally, use it as many times as needed:

```csharp
locusEffectsProvider.ShowLocusEffect( this, 
    this.RectangleToScreen( locusArea.Bounds ), 
                         "CustomArrow_Curved" );
```
                        
### Extensions: Creating a new Locus Effect type
If none of the predefined Locus Effect classes are satisfactory even after customization of their styles, then a developer can choose to add a new type of effect that inherits from the framework classes. This is more complex (but not too complex) and requires knowledge of the framework internals as described below. But once the new class is ready, the usage pattern is identical to creating and using a predefined Locus Effect as shown above.

***

## Locus Effects framework

![Locus Effects Class Diagram](https://github.com/bigman73/locus-effects/blob/master/images/LocusEffectsClassDiagram.png?raw=true)

### Framework Internals
The framework is built from a few core classes. LocusEffectsProvider is the main component and provides a facade for managing and controlling locus effects. When ShowLocusEffect is called LocusEffectsProvider starts showing the Locus Effect by delegating the call to the registered Locus Effect instance:

```csharp
    private void InternalShowLocusEffect( 
        Form activatorForm, 
        Rectangle locusScreenBounds, string locusEffectName)
    {
      BaseLocusEffect locusEffect = 
        m_registeredEffects[ locusEffectName ] as BaseLocusEffect;
      if ( locusEffect == null )
      {
          throw new ApplicationException( 
             string.Format( 
               "Could not show locus effect, 
                '{0}' is not registered", locusEffectName ));
      }

      this.InternalStopActiveLocusEffect();
      m_activeEffect = locusEffect ;
      locusEffect.ShowEffect(activatorForm, locusScreenBounds);
    }
```

which starts a new animation thread:

```csharp
    public virtual void ShowEffect( Form activatorForm, 
        Rectangle locusScreenBounds )
    {
        lock ( this )
        {
            m_runTimeData = new EffectRuntimeData() ;
            this.SetInitialRunTimeData() ;

            m_runTimeData.ActivatorForm = activatorForm ;
            m_runTimeData.LocusScreenBounds = locusScreenBounds ;
    
            // Create and start animation thread
            m_runTimeData.AnimationThread = new System.Threading.Thread( 
                new System.Threading.ThreadStart( DoAnimation ) ) ;
            m_runTimeData.AnimationThread.IsBackground = true ;
            m_runTimeData.AnimationThread.Priority = 
                ThreadPriority.AboveNormal ;
            m_runTimeData.AnimationThread.Name = "ShowEffect_Thread" ;
            m_runTimeData.AnimationThread.Start() ; 
         }
    }
```

```csharp
protected virtual void DoAnimation()
{
    try
    {
        m_owner.EffectWindow.SetEffect( this ) ;

        m_runTimeData.LastActivatorFormBounds =
             m_runTimeData.ActivatorForm.Bounds ;
        m_runTimeData.StepMaxDuration = 1000.0f /
             m_owner.FramesPerSecond ;

        this.SubscribeActivatorFormEvents();

        m_runTimeData.IsAnimating = true ;
        try
        {
             // Do the actual animation of the effect
             this.AnimateEffect() ;
        }
        finally
        {
            this.CleanUpEffect() ;
        }
     . . .
 }
```

So far, nothing exotic, but now we get to the fun stuff. BaseStandardEffect implements a standard effect, which is a normal sequence built up of Lead in, Body and Lead out animation stages. In each stage of the sequence there is a time duration. During the animation an accurate progress (step) is calculated based on the time. This is done in OnLeadInStep, OnAnimationBodyStep and OnLeadOutStep. The effect has paint methods for each stage that take the step progress into account and draw (i.e. render) the effect into an effect bitmap - PaintLeadIn, PaintBodyAnimation and PaintLeadOut.

```csharp
    protected override void AnimateEffect()
    {
        IntPtr activeWindowHandle = Win32NativeMethods.GetForegroundWindow();

        // Set initial bitmap
        m_animationStep = 0 ;
        using ( Graphics g = 
            Graphics.FromImage( m_runTimeData.EffectBitmap ) )
        {
            PaintLeadIn( g ) ;
        }

        // Move form out of visible area before showing it -
        //  We don't want the last bitmap
        m_owner.EffectWindow.ShowOutOfScreen() ;

        m_owner.EffectWindow.ApplyGraphics() ;
        
        Win32NativeMethods.SetForegroundWindow( activeWindowHandle ) ;
            
        m_startTime = DateTime.Now ;
        
        // Heart of a standard effect: (Lead in, body, lead out sequence)

        // 1. Lead in
        if ( m_leadInTime > 0 )
        {
            this.LeadIn() ;
        }

        // 2. Then, do some animation (body)
        if ( m_animationTime > 0 )
        {
            this.AnimateBody() ;
        }

        // 3. Finally, lead out
        if ( m_leadOutTime > 0 )
        {
            this.LeadOut() ;    
        }
    }
```

**m_owner** is an instance of the LocusEffectsProvider component, which owns an EffectWindow window. This special window is what makes the animation rock and roll. It is a per pixel alpha window or a layered window. Layered windows were first introduced in Windows 2000 and provide full support for RGBA bitmaps and transparency. They do not work via the regular WM_PAINT mechanism and are updated by setting a new bitmap to them each time an update is needed. This is in contrast to normal windows which have regions and work through WM_PAINT messages. So EffectWindow is a layered window which has features such as anchoring (i.e. setting the direction of the bitmap in relation to the locus area), bitmap manipulation (rotation, transparency control , shadow, color overlay). Each time a paint is triggered from the animation thread, the ApplyGraphics method of EffectWindow is called. This is how we update the bitmap of the window with the new rendered effect bitmap. CalculateBitmapBounds takes care of moving the effect bitmap to the correct position using anchoring mode rules. SetWindowBitmap does the actual Windows API call that sets the new effect bitmap on the layered window.

```csharp
protected virtual void AnimateBody()
{
    m_startAnimationTime = DateTime.Now ;

    TimeSpan duration ;

    DateTime stepStartTime ;
    while ( m_runTimeData != null && !m_runTimeData.StopRequested )
    {
        stepStartTime = DateTime.Now ;

        this.OnAnimationBodyStep() ;

        using ( Graphics g = Graphics.FromImage(
                   m_runTimeData.EffectBitmap ) )
        {
            PaintBodyAnimation( g ) ;
        }

        m_owner.EffectWindow.ApplyGraphics() ;

        duration = ( DateTime.Now - m_startAnimationTime ) ;
        if ( duration.TotalMilliseconds >= m_animationTime )
        {
            // Force draw of last position
            m_animationStep = 100.0f ;
            using ( Graphics g = Graphics.FromImage(
                 m_runTimeData.EffectBitmap ) )
            {
                PaintBodyAnimation( g ) ;
            }

            m_owner.EffectWindow.ApplyGraphics() ;

            break ;
        }
        else
        {
            TimeSpan stepDuration = ( DateTime.Now - stepStartTime ) ;

            // Allow the processor to rest - we don't want the
            //   animation thread to work all the time
            if ( stepDuration.TotalMilliseconds <
                    m_runTimeData.StepMaxDuration )
            {
                System.Threading.Thread.Sleep(
                     ( int ) ( m_runTimeData.StepMaxDuration -
                               stepDuration.TotalMilliseconds ) ) ;
            }
        }
    }
}
```

***ApplyGraphics*** - it's painting time!

```csharp
public virtual void ApplyGraphics()
{
    using ( Bitmap finalBitmap = new Bitmap( m_effect.EffectBitmap ))
    {

        // Calculate bounds of effect bitmap
        this.CalculateBitmapBounds( finalBitmap,
             m_effect.LocusScreenBounds ) ;

        // Handle anchoring logic of effect bitmap
        this.HandleEffectBitmapAnchoring( finalBitmap ) ;

        Size backBitmapSize = finalBitmap.Size ;

        // Extend back bitmap size so it has space for the shadow
        if ( m_effect.ShowShadow )
        {
            backBitmapSize.Width += Math.Abs(
                m_effect.ShadowOffset.X ) ;
            backBitmapSize.Height += Math.Abs(
                m_effect.ShadowOffset.Y ) ;
        }

        bool bitmapsRecreated = false ;

        // Re-create back bitmap size
        if ( backBitmapSize != m_backBitmapSize )
        {
            this.RecreateBitmaps( backBitmapSize ) ;

            bitmapsRecreated = true ;
        }

        // Note: FromHdc is very important as it hooks to
        //   the DIB Section (Back bitmap) in the memory DC
        //   directly, without creating an intermediate buffer
        // Thank you Lou Amadio for helping out here!

        // Draw effect bitmap and its shadow on back bitmap
        using ( Graphics g = Graphics.FromHdc( m_memDC ) )
        {
            g.SmoothingMode = SmoothingMode.HighQuality ;

            // Clear the back bitmap
            if ( !bitmapsRecreated )
            {
                g.Clear( Color.Transparent ) ;
            }

            // Draw shadow before final bitmap is drawn
            this.DrawShadow( finalBitmap, g ) ;

            // Draw final effect bitmap
            g.DrawImage( finalBitmap,
                0,
                0,
                m_bitmapSize.Width,
                m_bitmapSize.Height ) ;
        }

        // Render bitmap to window
        this.SetWindowBitmap( m_effect.RunTimeData.Opacity ) ;
    }
}
```

and finally, the bitmap is rendered to the layered window:

```csharp
protected virtual void SetWindowBitmap( int opacity )
{
    // Calculate coordinates
    Win32NativeMethods.Point pointMemory = new
        Win32NativeMethods.Point( 0, 0 ) ;
    Win32NativeMethods.Point pointScreen = new
        Win32NativeMethods.Point(
            m_bitmapLocation.X, m_bitmapLocation.Y ) ;
    Win32NativeMethods.Size size = new Win32NativeMethods.Size(
        m_backBitmapSize.Width, m_backBitmapSize.Height ) ;

    // Create blending function
    Win32NativeMethods.BLENDFUNCTION blend = new
        Win32NativeMethods.BLENDFUNCTION() ;
    blend.BlendOp             = Win32Constants.AC_SRC_OVER  ;
    blend.BlendFlags          = 0 ;
    blend.SourceConstantAlpha = ( byte ) ( opacity * 255 / 100 ) ;
    blend.AlphaFormat         = Win32Constants.AC_SRC_ALPHA ;

    // Render the layered window, by supplying it a memory DC
    //    which contains updated LocusEffect bitmap
    Win32NativeMethods.UpdateLayeredWindow(
        this.Handle,
        m_screenDC, ref pointScreen, ref size,
        m_memDC, ref pointMemory, 0,
        ref blend,
        Win32Constants.ULW_ALPHA ) ;
}
```

## Things to note
EffectWindow inherits from a LayeredWindow class which inherits from NativeWindow. Inheritance from System.Windows.Forms.Form class is not a good idea since all sorts of problems occur. In particular a nasty parking window is created per thread(!). Also a native window is faster and has much less footprint and negative side effects caused by default .NET implementation. We do not need all the complexities of full message support.
ImageBlender was extended from its original source code so it supports RGBA bitmaps.
Concrete effect classes can override AnimateEffect and provide their own concrete implementation.

## Documentation
The framework is quite heavily documented, and uses NDoc to generated a compiled HTML file (LocusEffects.chm) which is provided with the project and includes documentation for each class, its methods and properties.

## Things I have learned
Performance
Performance in GDI+, .NET and Windows Forms can be very slow. GDI+ itself is slower than GDI, and is (currently) not accelerated by graphics cards. All the processing is done in CPU time. Windows Forms is a very productive wrapper around Win API but it is heavy and has a lot of implementation issues which are slow, because the framework is generic. One specific thing to note is the usage of Bitmaps. Bitmaps in Win Forms are wrappers to GDI+ Bitmaps. This is all OK, however when you try to move from a GDI+ Bitmap to a regular GDI Bitmap you need a handle (HBITMAP). Calling GetHBitmap() is going to work but with a heavy price. The image is copied to a new GDI Bitmap and then the handle is returned to that new GDI bitmap. This is slow. Add to this a call of SelectObject into the Device Context each time, and it is slow too. Finally, UpdateLayeredWindow is also slow by nature. (See MSDN help about it.)
So what I have done (using Lou Amadio's help) is I have created a DIBSection manually, and selected it into the Device Context once. Now when I render the bitmap in ApplyGraphics I use Graphics.FromHDC(). This API is fast because GDI+ detects that the Device Context is attached to a DIBSection underneath and it uses directly without an intermediate buffer. Using Graphics.FromBitmap() is slow as it creates an intermediate buffer! Beware!

Layered Windows - These cool creatures are nice but slow. Empiric tests show that bitmaps over 300x300 start to clog your CPU. So it does not mean that large Locus Effects cannot be used, but there is a small price to it. Until Microsoft does not improve that API to make it much faster, there is nothing that can be done on that subject. Luckily most Locus Effects are smaller than this size, so there is no big problem.
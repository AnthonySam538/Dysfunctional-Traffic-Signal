// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #1
// Program Name: Dysfunctional Traffic Signal

//Name of this file: DysfunctionalForm.cs
//Purpose of this file: Define the layout of the user interface (UI) window.
//Purpose of this entire program: This program shows a blinking red light. This program contains exactly one clock.

//Source files in this program: DysfunctionalForm.cs, DysfunctionalMain.cs
//The source files in this program should be compiled in the order specified below in order to satisfy dependencies.
//  1. DysfunctionalForm.cs compiles into library file DysfunctionalForm.dll
//  2. DysfunctionalMain.cs compiles and links with the dll file above to create Dysfunctional.exe
//Compile this file:
//mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:DysfunctionalForm.dll DysfunctionalForm.cs

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class DysfunctionalForm : Form
{
  // Set the size of the window (Also used with positioning various elements)
  private const int formHeight = 900;
  private const int formWidth = formHeight * 16/9; //creates a 16:9 aspect ratio

  // Create Controls (Graphical objects on screen that provide the interface to the user)
  private Button pauseButton = new Button();
  private Button exitButton = new Button();
  private Label title = new Label();

  // Create Timer (It makes the light blink)
  private static System.Timers.Timer myTimer = new System.Timers.Timer(1000);

  // Create the Rectangle and SolidBrush used in drawing the circle
  private static Rectangle myRectangle = new Rectangle(formWidth/2 - formHeight*7/10 / 2, formHeight/2 - formHeight*7/10 / 2, formHeight * 7/10, formHeight * 7/10); //creates a Rectangle centered in the middle | also used in Invalidate()
  private static SolidBrush paint_brush = new SolidBrush(Color.Red);

  // This is the button size for all buttons on the form
  Size myButtonSize = new Size(85, 25);

  public DysfunctionalForm() //The constructor of this class
  {
    // Set up the form/window
    Text = "Dysfunctional Traffic Signal"; //name of the form/window
    Size = new Size(formWidth,formHeight); //size of the form/window
    BackColor = Color.LawnGreen; //background color of the form/window

    // Set up the title
    title.Text = "Dysfunctional Traffic Signal by Anthony Sam";
    title.Size = new Size(formWidth, formHeight / 10);
    title.Location = new Point(0,0);
    title.BackColor = Color.Cyan;
    title.TextAlign = ContentAlignment.MiddleCenter;

    // Set up the pause button
    pauseButton.Text = "Pause";
    pauseButton.Size = myButtonSize;
    pauseButton.Location = new Point(formWidth/2 - 2*myButtonSize.Width, formHeight*19/20 - myButtonSize.Height/2); //in the middle of the yellow band
    pauseButton.BackColor = Color.DarkOrchid;

    // Set up the exit button
    exitButton.Text = "Exit";
    exitButton.Size = myButtonSize;
    exitButton.Location = new Point(pauseButton.Location.X + 3*myButtonSize.Width, pauseButton.Location.Y); //you can fit 2 more buttons inbetween
    exitButton.BackColor = Color.Magenta;

    // Add the controls to the form
    Controls.Add(title);
    Controls.Add(pauseButton);
    Controls.Add(exitButton);

    // Tell the events which method to call (Each method is defined below)
    myTimer.Elapsed += new ElapsedEventHandler(toggleLight);
    pauseButton.Click += new EventHandler(pauseLight);
    exitButton.Click += new EventHandler(exitProgram);

    // Start the clock
    myTimer.Enabled = true;
  }

  // This method illustrates the screen
  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    graphics.FillRectangle(Brushes.Yellow, 0, formHeight * 9/10, formWidth, formHeight/10); //creates a yellow rectangle at the bottom
    graphics.FillEllipse(paint_brush, myRectangle);

    // This calls the superclass's OnPaint()
    base.OnPaint(e);
  }

  // When myTimer has ticked
  protected void toggleLight(Object sender, ElapsedEventArgs evt)
  {
    if(paint_brush.Color != Color.Transparent)
    {
      System.Console.WriteLine("The timer has toggled the light. The light will now turn off.");
      paint_brush.Color = Color.Transparent;
    }
    else
    {
      System.Console.WriteLine("The timer has toggled the light. The light will now turn on.");
      paint_brush.Color = Color.Red;
    }

    Invalidate(myRectangle);
  }

  // When pauseButton is clicked
  protected void pauseLight(Object sender, EventArgs events)
  {
    if(myTimer.Enabled)
    {
      System.Console.WriteLine("You clicked on the Pause button.");
      myTimer.Stop();
      pauseButton.Text = "Resume";
    }
    else
    {
      System.Console.WriteLine("You clicked on the Resume button.");
      myTimer.Start();
      pauseButton.Text = "Pause";
    }
  }

  // When exitButton is clicked
  protected void exitProgram(Object sender, EventArgs events)
  {
    System.Console.WriteLine("You clicked on the Exit button. This program will now end.");
    Close();
  }
}

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
  // Create the needed Rectangles and SolidBrush
  private static Rectangle yellowRectangle; //used for the yellow bar at the bottom
  private static Rectangle redCircle; //used for drawing the blinking light
  private static SolidBrush paintbrush = new SolidBrush(Color.Red); //makes the blinking light red

  // Create Controls (Graphical objects on screen that provide the interface to the user)
  private Button pauseButton = new Button();
  private Button exitButton = new Button();
  private Label title = new Label();

  // Create Timer (It makes the light blink)
  private static System.Timers.Timer myTimer = new System.Timers.Timer(1000);

  public DysfunctionalForm() //The constructor of this class
  {
    // Set up the form/window
    Height = 900; //height of the form/window (in pixels)
    Width = Height * 16/9; //width of the form/window (in pixels)
    Text = "Dysfunctional Traffic Signal"; //name of the form/window
    BackColor = Color.LawnGreen; //background color of the form/window

    // Set up the two Rectangles
    yellowRectangle.Size = new Size(Width, Height/10); //takes up 10% of the form
    yellowRectangle.Location = new Point(0, Height-yellowRectangle.Height); //at the bottom
    redCircle.Size = new Size(Height * 7/10, Height * 7/10); //70% of Height, since Height < Width
    redCircle.Location = new Point((Width-redCircle.Width)/2, (Height-redCircle.Height)/2); //in the middle of the center

    // Set up the title label
    title.Text = "Dysfunctional Traffic Signal by Anthony Sam";
    title.Size = yellowRectangle.Size;
    title.BackColor = Color.Cyan;
    title.TextAlign = ContentAlignment.MiddleCenter;

    // Set up the exit button (a bit out of order, I know)
    exitButton.Text = "Exit";
    exitButton.Location = new Point(Width/2 + exitButton.Width, yellowRectangle.Top+(yellowRectangle.Height-pauseButton.Height)/2); //in the middle of the yellow band
    exitButton.BackColor = Color.Magenta;

    // Set up the pause button
    pauseButton.Text = "Pause";
    pauseButton.Location = new Point(exitButton.Left - 3*pauseButton.Width, exitButton.Top); //you can fit 2 more buttons inbetween
    pauseButton.BackColor = Color.DarkOrchid;

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

    graphics.FillRectangle(Brushes.Yellow, yellowRectangle); //creates a yellow rectangle at the bottom
    graphics.FillEllipse(paintbrush, redCircle);

    // This calls the superclass's OnPaint()
    base.OnPaint(e);
  }

  // When myTimer has ticked
  protected void toggleLight(Object sender, ElapsedEventArgs evt)
  {
    if(paintbrush.Color != Color.Transparent)
    {
      System.Console.WriteLine("The timer has toggled the light. The light will now turn off.");
      paintbrush.Color = Color.Transparent;
    }
    else
    {
      System.Console.WriteLine("The timer has toggled the light. The light will now turn on.");
      paintbrush.Color = Color.Red;
    }

    Invalidate(redCircle);
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

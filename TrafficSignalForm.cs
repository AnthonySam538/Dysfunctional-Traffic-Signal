// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #2
// Program Name: Traffic Signal
//
// Name of this file: TrafficSignalMain.cs
// Purpose of this file: Launch the form where the traffic signal will be displayed.
// Purpose of this entire program: Display a traffic signal. This program contains exactly one clock.
// Source files in this program: TrafficSignalForm.cs, TrafficSignalMain.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class TrafficSignalForm : Form
{
  // Set the size of the window (Also used with positioning various elements)
  private const int formHeight = 900;
  private const int formWidth = formHeight * 9/16; //creates a 9:16 aspect ratio

  // Create the needed Rectangle and SolidBrush objects
  private static Rectangle topLight;
  private static Rectangle middleLight;
  private static Rectangle bottomLight;
  private static SolidBrush topColor = new SolidBrush(Color.DarkGray);
  private static SolidBrush middleColor = new SolidBrush(Color.DarkGray);
  private static SolidBrush bottomColor = new SolidBrush(Color.DarkGray);

  // Create Controls (listed from top to bottom, left to right)
  private Label title = new Label();
  private Button startButton = new Button();
  private GroupBox radioButtonBox = new GroupBox();
  private RadioButton slowButton = new RadioButton();
  private RadioButton fastButton = new RadioButton();
  private Button pauseButton = new Button();
  private Button exitButton = new Button();

  // Create Timer
  private static System.Timers.Timer myTimer = new System.Timers.Timer(8000);

  // Miscellaneous stuff
  private static Size myButtonSize = new Size(85, 25);
  private static int distanceBetweenButtons = formWidth/5-myButtonSize.Width;

  public TrafficSignalForm()
  {
    // Set up the form/window
    Text = "Traffic Signal";
    Size = new Size(formWidth,formHeight);
    BackColor = Color.Black;

    // Set up the Rectangles
    topLight.Size = new Size(formWidth/2, formWidth/2);
    topLight.Location = new Point(formWidth/4, formHeight/10);
    middleLight.Size = topLight.Size;
    middleLight.Location = new Point(topLight.Left, topLight.Bottom + formHeight/10);
    bottomLight.Size = topLight.Size;
    bottomLight.Location = new Point(topLight.Left, middleLight.Bottom + formHeight/10);

    // Set up the title label
    title.Text = "Traffic Signal by Anthony Sam";
    title.Size = new Size(formWidth, formHeight/10);
    title.Location = new Point(0,0);
    title.BackColor = Color.LawnGreen;
    title.TextAlign = ContentAlignment.MiddleCenter;

    // Set up the start button
    startButton.Text = "Start";
    startButton.Size = myButtonSize;
    startButton.Location = new Point(distanceBetweenButtons, formHeight*19/20 - myButtonSize.Height/2);
    startButton.BackColor = Color.Magenta;

    // Set up the GroupBox and its RadioButtons
    radioButtonBox.Text = "Rate of Change";
    radioButtonBox.Size = new Size(myButtonSize.Width*2, myButtonSize.Height*2);
    radioButtonBox.Location = new Point(startButton.Right+distanceBetweenButtons, formHeight*19/20 - radioButtonBox.Height/2);
    radioButtonBox.BackColor = startButton.BackColor;

    slowButton.Text = "Slow";
    slowButton.Size = new Size(radioButtonBox.Width/2, myButtonSize.Height);
    slowButton.Location = new Point(0, myButtonSize.Height);

    fastButton.Text = "Fast";
    fastButton.Size = slowButton.Size;
    fastButton.Location = new Point(myButtonSize.Width, myButtonSize.Height);

    // Set up the pause button
    pauseButton.Text = "Pause";
    pauseButton.Size = myButtonSize;
    pauseButton.Location = new Point(radioButtonBox.Right+distanceBetweenButtons, startButton.Top);
    pauseButton.BackColor = startButton.BackColor;

    // Set up the exit button
    exitButton.Text = "Exit";
    exitButton.Size = myButtonSize;
    exitButton.Location = new Point(pauseButton.Right+distanceBetweenButtons, startButton.Top);
    exitButton.BackColor = startButton.BackColor;

    // Add the RadioButtons to the GroupBox
    radioButtonBox.Controls.Add(slowButton);
    radioButtonBox.Controls.Add(fastButton);

    // Add the controls to the form
    Controls.Add(title);
    Controls.Add(startButton);
    Controls.Add(pauseButton);
    Controls.Add(exitButton);
    Controls.Add(radioButtonBox);

    // Tell the events which method to call
    myTimer.Elapsed += new ElapsedEventHandler(toggleLight);
    startButton.Click += new EventHandler(start);
    slowButton.CheckedChanged += new EventHandler(slowDown);
    fastButton.CheckedChanged += new EventHandler(speedUp);
    pauseButton.Click += new EventHandler(pause);
    exitButton.Click += new EventHandler(exit);
  }

  // This method illustrates the screen
  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    graphics.FillEllipse(topColor, topLight);
    graphics.FillEllipse(middleColor, middleLight);
    graphics.FillEllipse(bottomColor, bottomLight);

    graphics.FillRectangle(Brushes.Cyan, 0, formHeight - formHeight/10, formWidth, formHeight/10);

    // This calls the superclass's OnPaint()
    base.OnPaint(e);
  }

  protected void toggleLight(Object sender, ElapsedEventArgs e)
  {
    System.Console.WriteLine("The timer has toggled the light.");
    if(topColor.Color != Color.DarkGray) //light is red (change to green)
    {
      topColor.Color = Color.DarkGray;
      bottomColor.Color = Color.Green;
      myTimer.Interval *= 3/4;
    }
    else if(middleColor.Color != Color.DarkGray) //light is yellow (change to red)
    {
      topColor.Color = Color.Red;
      middleColor.Color = Color.DarkGray;
      myTimer.Interval *= 4;
    }
    else //light is green (change to yellow)
    {
      middleColor.Color = Color.Yellow;
      bottomColor.Color = Color.DarkGray;
      myTimer.Interval /= 3;
    }
    Invalidate();
  }

  protected void start(Object sender, EventArgs e)
  {
    System.Console.WriteLine("You clicked on the Start button.");
    myTimer.Interval = 8000;
    myTimer.Enabled = true;
    slowButton.Checked = true;
    topColor.Color = Color.Red;
    Invalidate();
  }

  protected void exit(Object sender, EventArgs e)
  {
    System.Console.WriteLine("You clicked on the Exit button. This program will now end.");
    Close();
  }

  protected void slowDown(Object sender, EventArgs e)
  {
    myTimer.Interval /= 2;
  }

  protected void speedUp(Object sender, EventArgs e)
  {
    myTimer.Interval *= 2;
  }

  protected void pause(Object sender, EventArgs e)
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
}

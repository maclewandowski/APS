

// Import the assembly that contains the parser
using info.lundin.Math;

// some other imports
using System;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

public class SpeedTest : Form 
{

	TextBox func, xmin, xmax, iter, ta;
	Label lbl1, lbl2, lbl3, lbl4;
	Button but;
	
	public SpeedTest()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		ta = new TextBox();
		func = new TextBox();
		xmin = new TextBox();
		xmax = new TextBox();
		iter = new TextBox();

		func.Text = "xcos(x)";
		xmin.Text = "-10";
		xmax.Text = "10";
		iter.Text = "1000";

		ta.Multiline = true;
		ta.ReadOnly = true;
		ta.ScrollBars = ScrollBars.Both;

		ta.Size	= new Size( 280 , 100 );
		func.Size = new Size( 225, 20 );
		xmin.Size = new Size( 40 , 20 );
		xmax.Size = new Size( 40 , 20 );
		iter.Size = new Size( 40 , 20 );
		
		but = new Button();
		lbl1 = new Label();
		lbl2 = new Label();
		lbl3 = new Label();
		lbl4 = new Label();

		but.TextAlign = ContentAlignment.MiddleCenter;

		lbl1.Size = new Size( 55 , 20 );
		lbl2.Size = new Size( 40 , 20 );
		lbl3.Size = new Size( 40 , 20 );
		lbl4.Size = new Size( 55 , 20 );

		but.Text = "Start";
		lbl1.Text = "Function:";
		lbl2.Text = "Xmin:";
		lbl3.Text = "Xmax:";
		lbl4.Text = "Iterations:";

		but.Size = new Size( 280, 20 );

		but.Click += new System.EventHandler( this.btn_Click ); 

		ta.Location = new Point( 5, 80 );
		func.Location = new Point( 60, 5 );
		xmin.Location = new Point( 50, 30 );
		xmax.Location = new Point( 140, 30 );
		iter.Location = new Point( 245, 30 );
		but.Location = new Point( 5, 55 );
		lbl1.Location = new Point( 5, 5 );
		lbl2.Location = new Point( 5, 30 );
		lbl3.Location = new Point( 95, 30 );
		lbl4.Location =  new Point( 185, 30 );

		this.Size = new Size( 300, 215 );
		this.Text = "SpeedTest - info.lundin.Math";
		this.StartPosition = FormStartPosition.CenterScreen;

		this.MaximizeBox = false;
		
		xmin.BackColor = Color.LightGray; 
		xmax.BackColor = Color.LightGray; 
		iter.BackColor = Color.LightGray; 
		func.BackColor = Color.LightGray; 
		ta.BackColor = Color.LightGray; 
		this.BackColor = Color.Indigo;
		
		lbl1.ForeColor = Color.White;
		lbl2.ForeColor = Color.White;
		lbl3.ForeColor = Color.White;
		lbl4.ForeColor = Color.White;
		but.BackColor = Color.LightGray;
	
		this.Controls.Add( ta );
		this.Controls.Add( func );
		this.Controls.Add( xmin );
		this.Controls.Add( xmax );
		this.Controls.Add( iter );
		this.Controls.Add( but );
		this.Controls.Add( lbl1 );
		this.Controls.Add( lbl2 );
		this.Controls.Add( lbl3 );
		this.Controls.Add( lbl4 );
	}
	
	public void 
	btn_Click( object sender, EventArgs e )
	{
		this.Cursor = Cursors.WaitCursor;
		runTest();
		this.Cursor = Cursors.Default;
	}



	public static void 
	Main( String[] args )
	{
		Application.Run( new SpeedTest() );
	}



	private void
	runTest()
	{	
		String funcstr;
		DateTime before, after;
		double xval, minx, maxx, step;
		Hashtable h;
		ExpressionParser e;
		int i, itr;

		try{
			
			e = new ExpressionParser();
			h = new Hashtable();

			funcstr = func.Text.Trim();

			minx = Double.Parse( xmin.Text.Trim() );
			maxx = Double.Parse( xmax.Text.Trim() );

			itr = Int32.Parse( iter.Text.Trim() );

			xval = minx;

			step = ( Math.Abs( minx ) + Math.Abs( maxx ) ) / itr;

			before = DateTime.Now;

			h.Add( "x" , "" );
			
			for( i = 0; i < itr; i++ )
			{
				h[ "x" ] = xval.ToString();
				e.Parse( funcstr , h );
				xval += step;
			}

			after = DateTime.Now;
	
			ta.Text = "Function: " + funcstr + "\r\n";
			ta.Text +=  "Xmin: " + minx + ", Xmax: " + maxx + "\r\n";
			ta.Text +=  "Iterations: " + itr + "\r\n";
			ta.Text +=  "Time consumed: " + Math.Round(((after.Subtract(before)).Ticks/1E4)) + " milliseconds.\r\n";
		}
		catch( Exception ex )
		{
			ta.Text = "Error: " + ex.Message;
		}
		
	}

	
}
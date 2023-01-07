using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
#region
Form scherm = new Form();
scherm.Text = "Reversi";
scherm.BackColor = Color.Gainsboro;
scherm.Icon = new Icon("favicon.ico");
scherm.ClientSize = new Size(800, 700);

Panel board = new Panel();
board.Location = new Point(150, 100);
board.Size = new Size(500, 500);
board.BackColor = Color.ForestGreen;

Button help = new Button();
help.Location = new Point(25, 200);
help.Size = new Size(100, 50);
help.Text = "Help";

Button rules = new Button();
rules.Location = new Point(25, 325);
rules.Size = new Size(100, 50);
rules.Text = "Rules";

Button newgame = new Button();
newgame.Location = new Point(25, 450);
newgame.Size = new Size(100, 50);
newgame.Text = "New Game";

Label boardsize = new Label();
boardsize.Location = new Point(695, 170);
boardsize.Size = new Size(100, 50);
boardsize.Text = "Board size:";

Button size4 = new Button();
size4.Location = new Point(675, 205);
size4.Size = new Size(100, 50);
size4.Text = "4 x 4";

Button size6 = new Button();
size6.Location = new Point(675, 285);
size6.Size = new Size(100, 50);
size6.Text = "6 x 6";

Button size8 = new Button();
size8.Location = new Point(675, 365);
size8.Size = new Size(100, 50);
size8.Text = "8 x 8";

Button size10 = new Button();
size10.Location = new Point(675, 445);
size10.Size = new Size(100, 50);
size10.Text = "10 x 10";

scherm.Controls.Add(board);
scherm.Controls.Add(help);
scherm.Controls.Add(rules);
scherm.Controls.Add(newgame);
scherm.Controls.Add(boardsize);
scherm.Controls.Add(size4);
scherm.Controls.Add(size6);
scherm.Controls.Add(size8);
scherm.Controls.Add(size10);

//Players
Label PlayerBl = new Label();
PlayerBl.Location = new Point(45, 20);
PlayerBl.Size = new Size(60, 60);
scherm.Controls.Add(PlayerBl);

Label PlayerWh = new Label();
PlayerWh.Location = new Point(695, 20);
PlayerWh.Size = new Size(60, 60);
scherm.Controls.Add(PlayerWh);

void player1(object sender, PaintEventArgs pea)
{ 
    pea.Graphics.FillEllipse(Brushes.Black, 0, 0, 60, 60);
} PlayerBl.Paint += player1;

void player2(object sender, PaintEventArgs pea)
{
    pea.Graphics.FillEllipse(Brushes.White, 0, 0, 60, 60);
} PlayerWh.Paint += player2;

Label zet = new Label();
zet.Location = new Point(360, 40);
zet.Size = new Size(200, 20);
scherm.Controls.Add(zet);

Label stenenwit = new Label();
stenenwit.Location = new Point(620, 40);
stenenwit.Size = new Size(60, 60);
stenenwit.Text = "werkt dit";
scherm.Controls.Add(stenenwit);

Label stenenzwart = new Label();
stenenzwart.Location = new Point(130, 40);
stenenzwart.Size = new Size(60, 60);
stenenzwart.Text = "ja dus";
scherm.Controls.Add(stenenzwart);
#endregion

int n = 10;
int[,] velden = new int[n, n];

InitializeBoard(); //board.Invalidate();

void click4(object o, EventArgs ea)
{
    n = 4;
    InitializeBoard();
    board.Invalidate();
}size4.Click += click4;

void click6(object o, EventArgs ea)
{
    n = 6;
    InitializeBoard();
    board.Invalidate();
}size6.Click += click6;

void click8(object o, EventArgs ea)
{
    n = 8;
    InitializeBoard();
    board.Invalidate();
}size8.Click += click8;

void click10(object o, EventArgs ea)
{
    n = 10;
    InitializeBoard();
    board.Invalidate();
}size10.Click += click10;

void playagain(object o, EventArgs ea)
{
    InitializeBoard();
    board.Invalidate();

} newgame.Click += playagain;
void InitializeBoard()
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            velden[i, j] = 0;
        }
    }

    velden[n / 2 - 1, n / 2 - 1] = 1;
    velden[n / 2, n / 2] = 1;
    velden[n / 2 - 1, n / 2] = 2;
    velden[n / 2, n / 2 - 1] = 2;
}

void DrawBoard(object o, PaintEventArgs pea)
{
    Graphics gr = pea.Graphics;
    Panel board = (Panel)o;
    Pen lijnen = new Pen(Color.Black, 2);
    int breedte = board.Width / n;
    int hoogte = board.Height / n;

    for (int x = 0; x < breedte; x++)
    {
        for (int y = 0; y < hoogte; y++)
        {
            gr.DrawRectangle(lijnen, x * breedte, y * hoogte, breedte, hoogte);
        }
    }
    for (int x = 0; x < n; x++)
    {
        for (int y = 0; y < n; y++)
        {
            if (velden[x, y] == 1)
            { gr.FillEllipse(Brushes.Black, x * breedte + 5, y * hoogte + 5, breedte - 10, hoogte - 10); }

            else if (velden[x, y] == 2)
            { gr.FillEllipse(Brushes.White, x * breedte + 5, y * hoogte + 5, breedte - 10, hoogte - 10); }
        }

    }

}

int beurt = 1;
void zetsteen(object sender, MouseEventArgs mea)
{
    int x = n * mea.X / board.Width;
    int y = n * mea.Y / board.Height;
   
        if (beurt == 1)
            { velden[x, y] = 1; }

        else if (beurt == 2)
            { velden[x, y] = 2; }
        
    board.Invalidate();
}
board.MouseClick += zetsteen;

//methode om van een int voor de speler een string te maken
string Beurtstring()
{
    if (beurt == 1)
        return "Black's turn";
    else
        return "White's turn";
}zet.Text = Beurtstring();

void showrules(object o, EventArgs e)
{
    string ruleslist = "1. Each reversi piece has a black and a white side. \n" +
        "2. Each player is assigned either a black or white color. \n" +
        "3. Players take turns placing their pieces on an empty tile such that two of their own pieces surround the opponent's pieces horizontally/vertically/diagonally. \n" +
        "4. When opponent's pieces are surrounded, they will flip to the same color as your own pieces\n" +
        "5. If the opponent has no available turns, you continue taking turns. \n" +
        "6. Once both players cannot make any more moves, the game ends. \n" +
        "7. The player with the most pieces of their color on the board wins!";
    MessageBox.Show(ruleslist);
}
rules.Click += showrules;
board.Paint += DrawBoard;

Application.Run(scherm);
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

//Buttons, labels en panel aanmaken
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

Label zet = new Label();
zet.Location = new Point(360, 40);
zet.Size = new Size(200, 20);

scherm.Controls.Add(board);
scherm.Controls.Add(help);
scherm.Controls.Add(rules);
scherm.Controls.Add(newgame);
scherm.Controls.Add(boardsize);
scherm.Controls.Add(size4);
scherm.Controls.Add(size6);
scherm.Controls.Add(size8);
scherm.Controls.Add(size10);
scherm.Controls.Add(zet);

Label PlayerBl = new Label();
PlayerBl.Location = new Point(45, 20);
PlayerBl.Size = new Size(60, 60);

Label PlayerWh = new Label();
PlayerWh.Location = new Point(695, 20);
PlayerWh.Size = new Size(60, 60);

Label statsbl = new Label();
statsbl.Location = new Point(130, 40);
statsbl.Size = new Size(60, 60);

Label statswh = new Label();
statswh.Location = new Point(620, 40);
statswh.Size = new Size(60, 60);

void player1(object sender, PaintEventArgs pea)
{
    pea.Graphics.FillEllipse(Brushes.Black, 0, 0, 60, 60);
}

void player2(object sender, PaintEventArgs pea)
{
    pea.Graphics.FillEllipse(Brushes.White, 0, 0, 60, 60);
}

scherm.Controls.Add(PlayerBl);
scherm.Controls.Add(PlayerWh);
scherm.Controls.Add(statsbl);
scherm.Controls.Add(statswh);
PlayerBl.Paint += player1;
PlayerWh.Paint += player2;
#endregion

//Startpositie
int n = 6;
int[,] velden = new int[n, n];
int turn = 1;
InitializeBoard();

#region

//int turn omzetten naar string zodat het als tekst op het label kan
string turnstring()
{
    if (turn == 1)
        return "Black's turn";
    else
        return "White's turn";
}
zet.Text = turnstring();

//als n = i, dan i x i array die begint bij 0 en eindigt bij i - 1
void click4(object o, EventArgs ea) 
{
    n = 4;
    InitializeBoard();
    board.Invalidate();
}

void click6(object o, EventArgs ea) 
{
    n = 6;
    InitializeBoard();
    board.Invalidate();
}

void click8(object o, EventArgs ea)
{
    n = 8;
    InitializeBoard();
    board.Invalidate();
}

void click10(object o, EventArgs ea)
{
    n = 10;
    InitializeBoard();
    board.Invalidate();
}


void playagain(object o, EventArgs ea)
{
    InitializeBoard();
    board.Invalidate();
}

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

size4.Click += click4;
size6.Click += click6;
size8.Click += click8;
size10.Click += click10;
newgame.Click += playagain;
rules.Click += showrules;
#endregion

//Array voor het speelveld maken
void InitializeBoard()
{
    velden = new int[n, n];
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

    CountPieces();
}

//Speelveld en beginstenen tekenen
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

int breedte = board.Width / n;
int hoogte = board.Height / n;

//Stenen tellen van beide spelers
void CountPieces()
{
    int BlackPieces = 0;
    int WhitePieces = 0;

    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (velden[i, j] == 1)
            {
                BlackPieces++;
            }
            else if (velden[i, j] == 2)
            {
                WhitePieces++;
            }
        }
    //ints omzetten naar strings zodat t als tekst op de labels kan
    string BlackPiecesstr = BlackPieces.ToString(); 
    string WhitePiecesstr = WhitePieces.ToString();
    statsbl.Text = BlackPiecesstr;
    statswh.Text = WhitePiecesstr;
    
} 

void SwitchPlayer(bool moremoves = false)
{
    //Hier de legaliteit herchecken, en veld hertekenen
    bool nomoremoves = true;
    for (int x = 0; x < breedte; x++)
    {
        for (int y = 0; y < hoogte; y++)
        {
            //Als er nog geen legale zetten zijn, moet geenzetten gecheckt worden
            if (nomoremoves) 
            {
                if (CheckLegal(x, y))
                {
                    nomoremoves = false;
                }
            }
        }
    }

    if (!nomoremoves)
    {
        if (turn == 1)
            turn = 2;
        else if (turn == 2)
            turn = 1;
        CountPieces();
    }
    else
    {
        GameOver();
    }
}

void GameOver()
{
    int BlackPieces = 0;
    int WhitePieces = 0;
    CountPieces();
    if (BlackPieces > WhitePieces)
    {
        MessageBox.Show("Black wins!");
    }
    else if (WhitePieces > BlackPieces)
    { 
        MessageBox.Show("White wins!");
    }
    else if (BlackPieces == WhitePieces)
    { 
        MessageBox.Show("Remise!"); 
    }

}

//Check de legaliteit van het vlak op x, y in elke richting
//Zet is illegaal, behalve als er een legale richting gevonden wordt
bool CheckLegal(int x, int y)
{
    if (velden[x, y] != 0)
        return false; 
    for (int m = -1; m <= 1; m++)
        for (int n = -1; n <= 1; n++)
            if (!(n == 0 && m == 0))
                if (outflank(x + m, y + n, m, n, false, true))
                    return true;
    return false;
}

/*void Veld_Play(object o, MouseEventArgs mea)
{
    PlayReversi(mea.X, mea.Y); 
    SwitchPlayer();
   
}*/

//Vindt in elke richting een insluiter (stopt dus niet bij de eerste vinder) en speelt als gevonden
void PlayReversi(int x, int y)
{
    //We gaan in elke richting een insluiter zoeken, en als die wordt gevonden,
    //wordt speelstenen vanaf daar getriggert
    for (int m = -1; m <= 1; m++)
        for (int n = -1; n <= 1; n++)
            if (!(n == 0 && m == 0))
                outflank(x + m, y + n, m, n, true);
}

//Vind een insluitende steen met minstends één andere ertussen
bool outflank(int x, int y, int newx, int newy, bool play = false, bool firststone = true)
{
    if (x < 0 || y < 0 || x > n - 1 || y > n - 1)
    {
        //Hij zoekt buiten het veld, dus niet op tijd gevonden
        return false;
    }

    if (velden[x, y] == 0)
    {
        return false; //Als een vak leeg is kan het niet ingesloten worden
    }

    if (velden[x, y] == turn)
    {
        {
            //Als de aanliggende steen meteen van dezelfde kleur is, is outflank false
            if (firststone) return false;
            //return true;
        }
        //Er is een insluitende steen gevonden, als het een zet is moet er worden gespeeld
        //Anders alleen return
        if (outflank(x + newx, y + newy, newx, newy, play, false))
        {
            if (play)
            {
                Moves(x, y, newx, newy);
            }
            return true;
        }
        return false;
    }
    return false;
}

//Speel stenen terug tot de beurt wordt teruggevonden
void Moves(int x, int y, int newx, int newy)
{
    //Blijf stenen van naar eigen kleur zetten tot eigen kleur wordt teruggevonden
    //Speelstenen wordt aangeroepen door een legaliteitscheck, dat hoeft hier niet dubbel
    if (velden[x, y] != turn)
    {
        //In toestand.set zit een automatische invalidate, dus wordt meteen getekend
        velden[x, y] = turn;
        Moves(x + newx, y + newy, newx, newy);
    }
}
void zetsteen(object sender, MouseEventArgs mea)
{
    int x = n * mea.X / board.Width;
    int y = n * mea.Y / board.Height;

    if (turn == 1)
    { velden[x, y] = 1; }

    else if (turn == 2)
    { velden[x, y] = 2; }

    board.Invalidate();
    SwitchPlayer();

} board.MouseClick += zetsteen;

board.Paint += DrawBoard;
//board.MouseClick += Veld_Play;

Application.Run(scherm);
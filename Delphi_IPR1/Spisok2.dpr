program Spisok2;

uses
  Vcl.Forms,
  Unit1 in 'Unit1.pas' {Form1},
  Unit2 in 'Unit2.pas' {Form2},
  Tovar in 'Tovar.pas',
  Unit3 in 'Unit3.pas' {Form3};

{$R *.res}
var
Form1:TForm1;

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.

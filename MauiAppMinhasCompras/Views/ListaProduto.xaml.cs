using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

	protected async override void OnAppearing()
	{
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{

			Navigation.PushAsync(new Views.NovoProduto());

		}catch (Exception ex)
		{

			DisplayAlert("ops", ex.Message, "ok");
 		}
    }

	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		
		string q = e.NewTextValue;
		/* eu me perguntei o que esse clear faz, e descobrir, tiramdo ela do codigo, que ela, alem de
		ser responsavel por mostrar o item pesquisado, enquanto "oculta" os outros itens, sem ela, os itens
		nao pesquisados nao sao ocultados, inutilizando essa função, alem disso ele começa a apresentar
		o mesmo comportamento, de duplicar os itens da lista, de forma indefinida, parecida com o bug 
		visto quando se adiciona itens na lista.*/
		lista.Clear();

        List<Produto> tmp = await App.Db.search(q);

        tmp.ForEach(i => lista.Add(i));

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

		double soma = lista.Sum(i => i.Total);

		string msg = $"O Total é {soma:C}";

		DisplayAlert("TOtal dos Produtos", msg, "OK");

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		/*bom eu tentei fazer o botao: tentei aplicar a seguinte logica;
			criar uma variavel para referenciar o Id, depois apagar uzando o Delete la no SqLite
		
		 Resultado: nao consegui :(.
		apos isso eu resolvi verificar o gabarito na aula da ag5*/

		



    }
}
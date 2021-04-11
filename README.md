# VibeSelecao
Repositório criado para a seleção da Vibe

Autor: Raelson Farias de Araújo
Documento de referência: https://s3.amazonaws.com/gupy5/production/companies/688/emails/1617720284946/cd4c0a20-96e2-11eb-b772-097ab89d1a85/vibeseleomobile2021-01.pdf

Alguns pontos importantes sobre o projeto:

### O Projeto

O projeto foi inciado nesta sexta à noite (09/04) e concluído no domingo às 20:00 (11/04). Para agilizar o projeto, utilizei o **Xamarin.Craftz** que é uma biblioteca com implementações base para ViewModel, Elements (que são basicamente ViewModels focados apenas para as entidades do projeto), Behaviors, Pages, Dialogs, etc. Não utilizei nenhuma dependência externa para implementação do MVVM (como Prism, MVVMCross, etc.), usei apenas o que se encontra no **Xamarin.Craftz**.

Foi adotado para o projeto um esquema de IoC simples, utilizando o DependencyService para registrar e retornar as dependências do container. Os serviços básicos podem ser encontrados nas implementações do **BaseViewModel** no projeto **Xamarin.Craftz** e os serviços do projeto estão no **App.xaml.cs**.

Para este projeto não foram necessárias implementações específicas de plataforma além do próprio Splash Screen do Android e iOS.

### Visual Studio

Foi utilizado o Visual Studio Community 2019, na versão 16.9.3. Um detalhe que passou a valer para a versão 16.9 é que não é mais possível editar storyboards para o iOS pelo ambiente do Windows (o meu ambiente de trabalho principal), mas foi possível utilizar o código do storyboard de outro projeto meu para fazer o Splash Screen no iOS também.

### Xamarin Forms

A versão do Xamarin Forms foi a mais atual no momento (5.0.0.2012). Isso significa que para essa versão, o **Visual Studio 2017 não é suportado**. Será necessário utilizar a versão 2019 para garantir a compilação do projeto ou reduzir a versão do Xamarin Forms para 4.8.x ou menos.

Referência: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/release-notes/5.0/5.0.0#visual-studio-2017-no-longer-supported

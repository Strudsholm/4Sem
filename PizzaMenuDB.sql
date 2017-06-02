CREATE DATABASE PizzaMenu;

USE PizzaMenu;

CREATE TABLE [dbo].[Ingredients] (
    [Ingredients_ID] INT           IDENTITY (1, 1) NOT NULL,
    [IngredientName] NVARCHAR (50) NULL,
    [Amount]         INT           NULL,
    PRIMARY KEY CLUSTERED ([Ingredients_ID] ASC)
);

CREATE TABLE [dbo].[Pizza] (
    [Pizza_ID] INT          IDENTITY (1, 1) NOT NULL,
    [Title]    VARCHAR (50) NULL,
    [Pris]     INT          NULL,
    PRIMARY KEY CLUSTERED ([Pizza_ID] ASC)
);


CREATE TABLE [dbo].[PizzaIngredients] (
    [Pizz_ID]      INT NOT NULL,
    [Ingredien_ID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Pizz_ID] ASC, [Ingredien_ID] ASC),
    CONSTRAINT [FK_PizzaIngredients_Pizza] FOREIGN KEY ([Pizz_ID]) REFERENCES [dbo].[Pizza] ([Pizza_ID]),
    CONSTRAINT [FK_PizzaIngredients_Ingredients] FOREIGN KEY ([Ingredien_ID]) REFERENCES [dbo].[Ingredients] ([Ingredients_ID])
);

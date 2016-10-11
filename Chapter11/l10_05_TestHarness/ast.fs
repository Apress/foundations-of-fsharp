module Strangelights.ExpressionParser.Ast

type Expr = 
  | Ident of string 
  | Val of System.Double
  | Multi of Expr * Expr
  | Div of Expr * Expr
  | Plus of Expr * Expr
  | Minus of Expr * Expr

export enum TypeArticleDTOs {
  Alimentaire = 1,
  NonConsomable = 2, 
}

export interface ArticleCreateRequest {
  type: TypeArticleDTOs;
  name: string;
  price: number;
  storage: number | null;
  canTakeAway: boolean;
}

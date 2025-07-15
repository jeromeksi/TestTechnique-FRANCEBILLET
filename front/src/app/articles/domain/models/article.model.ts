export interface Article {
  reference: string;
  name: string;
  priceHT: number;
  priceTTC: number;
  storage: number;
  type: 'alimentaire' | 'non-alimentaire';
  venteEmporter?: boolean;
}

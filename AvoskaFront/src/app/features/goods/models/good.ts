export interface Good {
  id: string;
  name: string;
  categoryName: string;
  description: string;
  image: string;
  composition: string;
  weightPerGram: number | null;

  nutritionalValue: {
    proteins: string;
    fat: string;
    carbohydrates: string;
  };

  manufactureCountry: string;
  expirationDate: string;
}

export class SearchElements {
    productId: number;
    name: string;
    brandName: number;
    brandID: number;
    modelID: number;
    modelName: number;
    attributes: ProductAttributes[];
}
export interface ProductAttributes {
    attributeName: string;
    value: string;
}
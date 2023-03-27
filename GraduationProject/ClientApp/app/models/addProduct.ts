export class AddProduct {
    description: string;
    name: string;
    price: number;
    condition: number;
    productId: number;
    images: ImageProductViewModel[];
    isNegotiable: boolean;
    isReplacable: boolean;
}
export class ImageProductViewModel {
    value: string;
    name: string;
}



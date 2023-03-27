export class ProductInfo {
    userProductId: number;
    productId: number;
    name: string;
    price: number;
    condition: number;
    images: ProductImageInfo[];
    userId: string;
}

export class ProductImageInfo {
    image: string;
}
export class UserProduct {
    id: number;
    name: string;
    description: string;
    price: number;
    condition: number;
    userId: string;
    productId: number;
    userProductImages: UserProductImages[];
}

export class UserProductImages {
    images: string;
    userProductId: number;
}

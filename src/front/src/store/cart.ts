import { defineStore } from 'pinia';

interface CartItem {
    productId: number;
    name: string;
    quantity: number;
}

export const useCartStore = defineStore('cart', {
    state: () => ({
        items: [] as CartItem[],
    }),
    actions: {
        addToCart(product: { id: number; name: string }) {
            const item = this.items.find(i => i.productId === product.id);
            if (item) {
                item.quantity++;
            } else {
                this.items.push({ productId: product.id, name: product.name, quantity: 1 });
            }
        },
        clearCart() {
            this.items = [];
        }
    }
});

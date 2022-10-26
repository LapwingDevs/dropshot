import React, { useCallback, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getProductById } from '../../../../api/controllers/ProductsClient';
import { ProductDetailsDto } from '../../../../api/models/Products/ProductDetailsDto';

const Product = () => {
  const [product, setProduct] = useState<ProductDetailsDto | undefined>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const { productId } = useParams();

  const fetchProduct = useCallback(() => {
    if (productId) {
      getProductById(+productId).then((p) => {
        setProduct(p);
        setIsLoading(false);
      });
    }
  }, []);

  useEffect(() => {
    fetchProduct();
  }, [fetchProduct]);

  if (isLoading) {
    return <div>loading..</div>;
  }
  return (
    <div>
      <div>
        product details {product?.name}, {product?.description}, {product?.price}
      </div>
      <div>variants</div>
      {product && product?.variants.length > 0 && (
        <div>
          {product.variants.map((variant) => {
            return <div key={variant.id}>variant: {variant.size}</div>;
          })}
        </div>
      )}
    </div>
  );
};

export default Product;

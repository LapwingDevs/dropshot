import { Button, TextField } from '@mui/material';
import React, { useCallback, useEffect, useState } from 'react';
import { Controller, useForm } from 'react-hook-form';
import { useParams } from 'react-router-dom';
import { getProductById } from '../../../../api/controllers/ProductsClient';
import { addVariantToProduct, removeVariant } from '../../../../api/controllers/VariantsClient';
import { ProductDetailsDto } from '../../../../api/models/Products/ProductDetailsDto';
import { AddVariantRequest } from '../../../../api/models/Variants/AddVariantRequest';

interface IFormData {
  size: number;
}

const Product = () => {
  const [product, setProduct] = useState<ProductDetailsDto | undefined>(undefined);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const { productId } = useParams();
  const { handleSubmit, reset, control } = useForm<IFormData>();

  const fetchProduct = useCallback(() => {
    if (productId) {
      getProductById(+productId).then((p) => {
        setProduct(p);
        setIsLoading(false);
      });
    }
  }, []);

  const addVariant = (data: IFormData) => {
    if (productId === undefined) {
      console.log('alert');
      return;
    }

    const request: AddVariantRequest = {
      productId: +productId,
      size: data.size,
    };

    addVariantToProduct(request).then(() => {
      fetchProduct();
    });
  };

  const remove = (variantId: number) => {
    removeVariant(variantId).then(() => {
      fetchProduct();
    });
  };

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

      <br />

      <div>variants</div>
      {product && product?.variants.length > 0 && (
        <div>
          {product.variants.map((variant) => {
            return (
              <div key={variant.id}>
                <span>variant: {variant.size}</span>
                <span>
                  <Button onClick={() => remove(variant.id)}>Remove</Button>
                </span>
              </div>
            );
          })}
        </div>
      )}

      <br />

      <div>add new variant</div>
      <form>
        <span>
          <Controller
            name={'size'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField type={'number'} onChange={onChange} value={value} label={'size'} />
            )}
          />
        </span>

        <span>
          <Button onClick={handleSubmit((data) => addVariant(data))}>Submit</Button>
        </span>
      </form>
    </div>
  );
};

export default Product;

import { Button } from '@mui/material';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getProducts } from '../../../api/controllers/ProductsClient';
import { ProductOnListDto } from '../../../api/models/Products/ProductOnListDto';

const ProductsManagement = () => {
  const [products, setProducts] = useState<ProductOnListDto[]>([]);
  const navigate = useNavigate();

  const fetchProducts = useCallback(() => {
    getProducts().then((p) => {
      setProducts(p);
    });
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  return (
    <div>
      <Button onClick={() => navigate('new')} variant={'outlined'} style={{ color: 'black' }}>
        Add product
      </Button>
      <div>Products management</div>
      {products.map((product) => {
        return (
          <div key={product.id}>
            <span>- {product.name}</span>
            <span>
              <Button
                onClick={() => navigate(product.id.toString())}
                variant={'outlined'}
                style={{ color: 'black', marginLeft: '5px' }}
              >
                open
              </Button>
            </span>
          </div>
        );
      })}
    </div>
  );
};

export default ProductsManagement;

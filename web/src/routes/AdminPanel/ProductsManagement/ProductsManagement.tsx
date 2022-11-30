import { Button, Typography } from '@mui/material';
import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getProducts } from '../../../api/controllers/ProductsClient';
import { ProductOnListDto } from '../../../api/models/Products/ProductOnListDto';
import './ProductsManagement.scss';

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
    <div className="container">
      <Typography variant="h4" sx={{ marginBottom: '10px' }}>
        Products management
      </Typography>
      {products.map((product) => {
        return (
          <div className="item" key={product.id}>
            <Typography variant="h6">- {product.name}</Typography>
            <Button onClick={() => navigate(product.id.toString())}>open</Button>
          </div>
        );
      })}

      <Button onClick={() => navigate('new')}>Add product</Button>
    </div>
  );
};

export default ProductsManagement;

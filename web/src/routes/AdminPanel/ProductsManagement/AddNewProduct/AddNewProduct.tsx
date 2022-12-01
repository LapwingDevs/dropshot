import { Button, MenuItem, Select, FormControl, InputLabel, TextField, Typography } from '@mui/material';
import React from 'react';
import { Controller, useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import { addProduct } from '../../../../api/controllers/ProductsClient';
import { AddProductRequest } from '../../../../api/models/Products/AddProductRequest';
import { ClothesUnitOfMeasure } from '../../../../api/models/Products/ClothesUnitOfMeasure';
import './AddNewProduct.scss';

interface IFormData {
  name: string;
  description: string;
  price: number;
  unitOfSize: ClothesUnitOfMeasure;
}

const options = [
  {
    label: ClothesUnitOfMeasure[ClothesUnitOfMeasure.Number],
    value: ClothesUnitOfMeasure.Number,
  },
  {
    label: ClothesUnitOfMeasure[ClothesUnitOfMeasure.Letter],
    value: ClothesUnitOfMeasure.Letter,
  },
];

const AddNewProduct = () => {
  const { handleSubmit, reset, control } = useForm<IFormData>();

  const navigate = useNavigate();

  const onSubmit = (data: IFormData) => {
    const request: AddProductRequest = {
      name: data.name,
      description: data.description,
      price: data.price,
      unitOfSize: data.unitOfSize,
    };
    addProduct(request)
      .then(() => {
        navigate('/admin-panel/products-management');
      })
      .catch(() => {
        console.log('alert here');
      });
  };

  const generateSelectOptions = () => {
    return options.map((option) => {
      return (
        <MenuItem key={option.value} value={option.value}>
          {option.label}
        </MenuItem>
      );
    });
  };

  return (
    <div className="container">
      <Typography variant="h4" sx={{ marginBottom: '20px' }}>
        Add new product:
      </Typography>
      <form>
        <div>
          <Controller
            name={'name'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField sx={{ marginBottom: '10px' }} onChange={onChange} value={value} label={'name'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'description'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField sx={{ marginBottom: '10px' }} onChange={onChange} value={value} label={'description'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'price'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                sx={{ marginBottom: '10px' }}
                type={'number'}
                onChange={onChange}
                value={value}
                label={'price'}
              />
            )}
          />
        </div>

        <div>
          <Controller
            control={control}
            name={'unitOfSize'}
            render={({ field: { onChange, value } }) => (
              <FormControl sx={{ marginBottom: '10px' }}>
                <InputLabel>Unit of size</InputLabel>
                <Select onChange={onChange} value={value} style={{ width: '223px' }}>
                  {generateSelectOptions()}
                </Select>
              </FormControl>
            )}
          />
        </div>

        <div>
          <Button onClick={handleSubmit((data) => onSubmit(data))}>Submit</Button>
          <Button onClick={() => reset()} variant={'outlined'}>
            Reset
          </Button>
        </div>
      </form>
    </div>
  );
};

export default AddNewProduct;

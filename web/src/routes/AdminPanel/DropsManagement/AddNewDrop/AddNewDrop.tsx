import { Button, MenuItem, Select, TextField } from '@mui/material';
import React, { useState, useCallback, useEffect } from 'react';
import { Controller, useForm } from 'react-hook-form';
import { getWarehouseVariants } from '../../../../api/controllers/VariantsClient';
import { AddDropRequest } from '../../../../api/models/Drops/AddDropRequest';
import { CreateDropItemDto } from '../../../../api/models/Drops/CreateDropItemDto';
import { VariantDto } from '../../../../api/models/Variants/VariantDto';
import { addDays } from 'date-fns';
import { addDrop } from '../../../../api/controllers/DropsClient';
import { useNavigate } from 'react-router-dom';

interface IFormData {
  name: string;
  description: string;
  startDateTime: Date;
  endDateTime: Date;
  selectedVariants: number[];
}

interface SelectOption {
  label: string;
  value: number;
}

const AddNewDrop = () => {
  const [variants, setVariants] = useState<VariantDto[]>([]);

  const { control, handleSubmit } = useForm<IFormData>();
  const navigate = useNavigate();

  const fetchVariants = useCallback(() => {
    getWarehouseVariants().then((v) => {
      setVariants(v);
    });
  }, []);

  const generateSelectOptions = () => {
    const options: SelectOption[] = variants.map((variant) => {
      return { value: variant.variantId, label: `${variant.productName}, ${variant.size}` };
    });

    return options.map((option) => {
      return (
        <MenuItem key={option.value} value={option.value}>
          {option.label}
        </MenuItem>
      );
    });
  };

  const submitDrop = (data: IFormData) => {
    const request: AddDropRequest = {
      name: data.name,
      description: data.description,
      startDateTime: addDays(new Date(), -1),
      endDateTime: addDays(new Date(), 2),
      dropItems: data.selectedVariants.map((i) => {
        return { quantity: 1, variantId: i };
      }),
    };

    addDrop(request).then(() => {
      navigate('/admin-panel/drops-management');
    });
  };

  useEffect(() => {
    fetchVariants();
  }, [fetchVariants]);

  return (
    <div>
      <div>new drop</div>

      <form>
        <div>
          <Controller
            name={'name'}
            control={control}
            render={({ field: { onChange, value } }) => <TextField onChange={onChange} value={value} label={'name'} />}
          />
        </div>

        <div>
          <Controller
            name={'description'}
            control={control}
            render={({ field: { onChange, value } }) => <TextField onChange={onChange} value={value} label={'name'} />}
          />
        </div>

        <div>
          <Controller
            control={control}
            name={'selectedVariants'}
            render={({ field: { onChange, value } }) => (
              <Select
                multiple
                onChange={onChange}
                value={value}
                renderValue={(selected) => selected.join(', ')}
                defaultValue={[]}
              >
                {generateSelectOptions()}
              </Select>
            )}
          />
        </div>
        <Button onClick={handleSubmit((data) => submitDrop(data))}>Submit</Button>
      </form>
    </div>
  );
};

export default AddNewDrop;

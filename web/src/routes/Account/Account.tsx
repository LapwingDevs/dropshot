import React, { useContext, useMemo, useState } from 'react';
import AuthContext from '../../context/AuthContext';
import { Controller, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import * as yup from 'yup';
import { onlyLetters } from '../../constants/Regexes';
import { Button, TextField } from '@mui/material';
import { updateUser } from '../../api/controllers/UserClient';
import { useSnackbar } from 'notistack';
import ConfirmationDialog from '../../components/Common/ConfirmationDialog/ConfirmationDialog';

const validationSchema = yup.object({
  firstName: yup
    .string()
    .matches(onlyLetters, 'First name can only contain Latin letters.')
    .required('First name is required!'),
  lastName: yup
    .string()
    .matches(onlyLetters, 'Last name can only contain Latin letters.')
    .required('Last name is required!'),
});

interface IFormData {
  firstName: string;
  lastName: string;
  email: string;
  addressLine1: string;
  addressLine2: string;
  addressCity: string;
  addressPostalCode: string;
}

const Account = () => {
  const { enqueueSnackbar } = useSnackbar();
  const { currentUser } = useContext(AuthContext);

  const defaultValue = useMemo<IFormData>(
    () =>
      currentUser
        ? {
            firstName: currentUser.firstName,
            lastName: currentUser.lastName,
            email: currentUser.email,
            addressLine1: currentUser.address?.line1 ?? '',
            addressLine2: currentUser.address?.line2 ?? '',
            addressCity: currentUser.address?.city ?? '',
            addressPostalCode: currentUser.address?.postalCode ?? '',
          }
        : {
            firstName: '',
            lastName: '',
            email: '',
            addressLine1: '',
            addressLine2: '',
            addressCity: '',
            addressPostalCode: '',
          },
    [],
  );

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<IFormData>({
    defaultValues: useMemo(() => {
      return defaultValue;
    }, [defaultValue]),
    resolver: yupResolver(validationSchema),
  });

  const onSubmit = async (e: IFormData, event: React.BaseSyntheticEvent | undefined) => {
    event?.preventDefault();
    try {
      const updateUserDto = {
        id: currentUser?.id ?? -1,
        firstName: e.firstName,
        lastName: e.lastName,
        address: { line1: e.addressLine1, line2: e.addressLine2, city: e.addressCity, postalCode: e.addressPostalCode },
      };

      await updateUser(updateUserDto).then((data) => {
        enqueueSnackbar('Account successfully updated', { variant: 'success' });
      });
    } catch (e) {
      enqueueSnackbar(`${e}`, { variant: 'error' });
    }
  };

  return (
    <div>
      <div>Account info</div>
      <form>
        <div>
          <Controller
            name={'firstName'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                onChange={onChange}
                value={value}
                label={'First name'}
                helperText={errors.firstName?.message}
                error={!!errors.firstName}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'lastName'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                onChange={onChange}
                value={value}
                label={'Last name'}
                helperText={errors.lastName?.message}
                error={!!errors.lastName}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'email'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField type="email" onChange={onChange} value={value} label={'Email'} disabled={true} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressLine1'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField onChange={onChange} value={value} label={'Line 1'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressLine2'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField onChange={onChange} value={value} label={'Line 2'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressPostalCode'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField onChange={onChange} value={value} label={'Postal code'} />
            )}
          />
        </div>

        <div>
          <Controller
            name={'addressCity'}
            control={control}
            render={({ field: { onChange, value } }) => <TextField onChange={onChange} value={value} label={'City'} />}
          />
        </div>

        <Button type="submit" onClick={handleSubmit(onSubmit)}>
          Save changes
        </Button>
      </form>
    </div>
  );
};

export default Account;

import React, { useCallback, useState } from 'react';
import { Alert, CircularProgress, debounce, Snackbar, TextField, Typography } from '@mui/material';
import { UserDto } from '../../../../api/models/User/UserDto';
import { getUsers } from '../../../../api/controllers/UserClient';
import { promoteUser } from '../../../../api/controllers/AuthClient';
import UserCard from '../../../../components/UserManagment/UserCard/UserCard';
import { useSnackbar } from 'notistack';

const AddAdmin = () => {
  const { enqueueSnackbar } = useSnackbar();

  const [text, setText] = useState('');
  const [users, setUsers] = useState<UserDto[]>([]);
  const [usersOptionsLoading, setUsersOptionsLoading] = useState(false);

  const handleOnChangeUsersDebounced = useCallback(
    debounce(async (query) => {
      setUsersOptionsLoading(true);
      const response = await getUsers(query);
      if (response?.count > 0) {
        setUsers(response.users);
      } else {
        setUsers([]);
      }
      setUsersOptionsLoading(false);
    }, 800),
    [],
  );

  const handleInputChange = (e: any) => {
    if (e.target.value) {
      handleOnChangeUsersDebounced(e.target.value);
    } else {
      setUsers([]);
    }
    setText(e.target.value);
  };

  const handlePromoteClicked = async (user: UserDto) => {
    console.log('Promote');
    try {
      await promoteUser(user.email).then(() => {
        enqueueSnackbar('User successfully promoted', { variant: 'success' });
        setUsers(users.filter((item) => item !== user));
      });
    } catch (e) {
      enqueueSnackbar('Error occurred!', { variant: 'error' });
    }
  };

  return (
    <div className="list-container">
      <Typography sx={{ marginBottom: '15px' }} variant="h4">
        Search for admin:
      </Typography>
      <TextField onChange={handleInputChange} value={text} />
      <div>
        {usersOptionsLoading ? (
          <CircularProgress size={20} />
        ) : (
          users.map((user) => {
            return (
              <UserCard
                key={user.id}
                buttonLabel="Promote"
                buttonDisabled={false}
                onConfirm={() => handlePromoteClicked(user)}
                user={user}
              />
            );
          })
        )}
      </div>
    </div>
  );
};

export default AddAdmin;
